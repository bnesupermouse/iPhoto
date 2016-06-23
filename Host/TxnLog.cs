using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ServiceStack;
using ServiceStack.Text;

namespace TxnLog
{
    public class TxnLog
    {
        public static TxnLog GlbLog = new TxnLog();
        public string FileName;
        public string IndexFileName;
        public Result LogTxn(Tx tx)
        {
            EntityWriter writer = new EntityWriter();
            tx.Transfer(writer);
            string txnContent = writer.StringContent+"\r";
            StreamWriter txWriter = new StreamWriter(FileName, true);
            long BeginPos = txWriter.BaseStream.Position;
            txWriter.Write(txnContent);
            txWriter.Flush();
            long EndPos = txWriter.BaseStream.Position;
            txWriter.Close();

            StreamWriter idxWriter = new StreamWriter(IndexFileName, true);
            string Index = string.Format("{0},{1},{2},{3}\r", tx.TxnId, tx.SerialNo, BeginPos, txnContent.Length);
            idxWriter.Write(Index);
            idxWriter.Flush();
            idxWriter.Close();
            return Result.Success;
        }

        public Tx GetNextTxn(long lastSerialNo)
        {
            StreamReader idxReader = new StreamReader(IndexFileName);
            string Index;
            while ( ( Index = idxReader.ReadLine() ) != null )
            {
                string[] txPos = Index.Split(new char[] { ',' });
                if (Int64.Parse(txPos[1]) > lastSerialNo && txPos[0] == "1")
                {
                    TxTest curTx = new TxTest();
                    EntityReader reader = new EntityReader();



                    byte[] byData = new byte[200];
                    char[] charData = new Char[200];

                    try
                    {
                        Program.stopWatch.Start();

                        for (int i = 0; i < 10000; i++)
                        {
                            FileStream aFile = new FileStream(FileName, FileMode.Open);
                            aFile.Seek(Int64.Parse(txPos[2]), SeekOrigin.Begin);
                            aFile.Read(byData, 0, Int32.Parse(txPos[3]) - 1);
                            aFile.Close();
                        }
                        // Get the elapsed time as a TimeSpan value.
                        TimeSpan ts = Program.stopWatch.Elapsed;

                        // Format and display the TimeSpan value. 
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);
                        Console.WriteLine("RunTime " + elapsedTime);
                        Program.stopWatch.Stop();


                        reader.StringContent = System.Text.Encoding.UTF8.GetString(byData);
                        curTx.Transfer(reader);

                        

                        return (Tx)reader.CurEntity;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("An IO exception has been thrown!");
                        Console.WriteLine(e.ToString());
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
