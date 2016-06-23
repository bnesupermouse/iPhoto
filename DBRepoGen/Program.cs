using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace MemDBGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLScriptGenerator gen = new SQLScriptGenerator();
            var code = gen.ParseDB();
            string lines = code;

            // Write the string to a file.
            File.WriteAllText(@"..\..\..\HostDB\DBRepo.cs", String.Empty);
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\..\HostDB\DBRepo.cs");
            file.WriteLine(lines);

            file.Close();

            //Test
            //using (var context = new testDataContext())
            //{
            //    var workpic = context.PhotographerWorkPictures.Where(p => p.PhotographerPictureId == 1).First();
            //    workpic.SortOrder = 290;
            //    workpic.UpdateSql();
            //}
            Console.Read();
        }

        //static void ParseTable(string tableName)
        //{
        //    string memDBText = "";
        //    string deleteCommandStr = @"UPDATE " + tableName + " SET ";// paramColumn='@paramName' WHERE conditionColumn='@conditionName'";
        //    string whereCond = " where ";
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MemDBGenerator.Properties.Settings.iPhotoConnectionString"].ConnectionString;
        //    DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        //    var da = factory.CreateDataAdapter();

        //    using (DbConnection connection = factory.CreateConnection())
        //    using (DbCommand command = factory.CreateCommand())
        //    {
        //        command.CommandText = "select * from " + tableName;
        //        da.SelectCommand = command;
        //        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

        //        connection.ConnectionString = connectionString;
        //        connection.Open();
        //        command.Connection = connection;

        //        memDBText += @"SqlConnection sqlConn = new SqlConnection(connectionString);
        //        SqlCommand sqlComm = new SqlCommand();
        //        sqlComm = sqlConn.CreateCommand(); ";

        //        var dtab = new DataTable();
        //        da.FillSchema(dtab, SchemaType.Source);
        //        string whereClause = "";
        //        foreach (DataColumn col in dtab.Columns)
        //        {
        //            string name = col.ColumnName;
        //            bool isNull = col.AllowDBNull;
        //            bool isPrimary = dtab.PrimaryKey.Contains(col);

        //            if (isPrimary)
        //            {
        //                whereClause += "e." + name + " == " + name + " &&";
        //                whereCond += name + " = @" + name + " and ";
        //            }

        //            //UPDATE
        //            deleteCommandStr += name + " = @" + name + ", ";
        //            memDBText += "sqlComm.Parameters.AddWithValue(\"";
        //            memDBText += name + "\", ";
        //            memDBText += name+");\r\n";

        //        }
        //        whereCond = whereCond.Trim().Substring(0, whereCond.Length - 3);
        //        deleteCommandStr = deleteCommandStr.Trim().Substring(0, deleteCommandStr.Length - 1);
        //        deleteCommandStr += whereCond;

        //        if (whereClause.Trim().Length > 0)
        //        {
        //            whereClause = whereClause.Trim().Substring(0, whereClause.Length - 2);
        //        }



        //        //Generate code for UPDATE


        //        //Basic UPDATE method with Parameters
        //        memDBText += @"
        //        sqlComm.CommandText = deleteCommandStr;
        //        sqlConn.Open();
        //        sqlComm.ExecuteNonQuery();
        //        sqlConn.Close();";
                
        //    }
        //    Console.WriteLine(memDBText);
        //}
    }
}
