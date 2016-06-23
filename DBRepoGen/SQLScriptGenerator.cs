using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace MemDBGenerator
{
    public class SQLScriptGenerator
    {
        static public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MemDBGenerator.Properties.Settings.iPhotoConnectionString"].ConnectionString;
        static public DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        static public DbConnection connection = factory.CreateConnection();
        static public DbCommand command = factory.CreateCommand();
        public SQLScriptGenerator()
        {
            connection.ConnectionString = connectionString;
            connection.Open();
            command.Connection = connection;
        }
        public string ParseDB()
        {
            string dbStr = 
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace HostDB
{";

            DataTable table = connection.GetSchema("Tables");
            foreach (System.Data.DataRow row in table.Rows)
            {
                var curTable = GetTableSchema(row[2].ToString());
                dbStr += @"
    public partial class ";
                dbStr += row[2].ToString() + @"
    {";
                dbStr += ParseTable(curTable);
                dbStr += @"
    }
";
                
            }
            dbStr += "}";
            Console.Write(dbStr);
            return dbStr;
        }
        public TableSchema GetTableSchema(string tableName)
        {
            TableSchema SQLTable = new TableSchema();
            SQLTable.TableName = tableName;
            var da = factory.CreateDataAdapter();
            command.CommandText = "select * from " + tableName;
            da.SelectCommand = command;
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            var dtab = new DataTable();
            da.FillSchema(dtab, SchemaType.Source);
            foreach (DataColumn col in dtab.Columns)
            {
                string name = col.ColumnName;
                bool isNull = col.AllowDBNull;
                bool isPrimary = dtab.PrimaryKey.Contains(col);

                if (isPrimary)
                {
                    SQLTable.PrimaryKeys.Add(name);
                }
                SQLTable.Columns.Add(name);
            }
            return SQLTable;
        }

        public string ParseTable(TableSchema curTable)
        {
            string tableCode = "";
            tableCode += GetInsertScript(curTable);
            tableCode += "\r\n";
            tableCode += GetUpdateScript(curTable);

            tableCode += "\r\n";
            tableCode += GetDeleteScript(curTable);

            tableCode += "\r\n";
            tableCode += GetFetchScript(curTable);

            tableCode += "\r\n";
            tableCode += GetCompareScript(curTable);
            return tableCode;
        }

        public string GetInsertScript(TableSchema curTable)
        {
            string insertTxt = "";
            insertTxt +=
@"
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
";
            string insertCommandStr = @"INSERT INTO " + curTable.TableName + " ( ";// @"INSERT INTO tableName (paramColum) VALUES (@paramName)";
            string columnList = "";
            string paramList = "";

            foreach (var col in curTable.Columns)
            {
                //UPDATE
                columnList += col + ",";
                paramList += "@" + col + ",";
                //deleteCommandStr += col + " = @" + col + ", ";
                insertTxt += "            sqlComm.Parameters.AddWithValue(\"";
                insertTxt += col + "\", ";
                insertTxt += "(object)" + col + "??DBNull.Value);\r\n";
            }
            columnList = columnList.Substring(0, columnList.Length - 1);
            paramList = paramList.Substring(0, paramList.Length - 1);
            insertCommandStr += columnList + " ) " + "values (" + paramList + ")";

            insertTxt += @"
            sqlComm.CommandText =";
            insertTxt += "\"" + insertCommandStr + "\";\r\n";
            insertTxt += @"
            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }";

            return insertTxt;
        }

        public string GetUpdateScript(TableSchema curTable)
        {
            string updateTxt = "";
            updateTxt +=
@"
        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
";
            string updateCommandStr = @"UPDATE " + curTable.TableName + " SET ";// paramColumn='@paramName' WHERE conditionColumn='@conditionName'";
            string whereCond = " where ";

            foreach (var col in curTable.PrimaryKeys)
            {
                whereCond += col + " = @" + col + " and ";
            }
            foreach (var col in curTable.Columns)
            {
                //UPDATE
                updateCommandStr += col + " = @" + col + ", ";
                updateTxt += "            sqlComm.Parameters.AddWithValue(\"";
                updateTxt += col + "\", ";
                updateTxt += "(object)"+col + "??DBNull.Value);\r\n";
            }
            whereCond = whereCond.Substring(0, whereCond.Length - 4);
            updateCommandStr = updateCommandStr.Substring(0, updateCommandStr.Length - 2);
            updateCommandStr += whereCond;

            updateTxt += @"
            sqlComm.CommandText =";
            updateTxt += "\""+updateCommandStr+"\";\r\n";
            updateTxt += @"
            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }";

            return updateTxt;
        }

        public string GetDeleteScript(TableSchema curTable)
        {
            string deleteTxt = "";
            deleteTxt +=
@"
        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
";
            string deleteCommandStr = @"DELETE FROM " + curTable.TableName + " ";// paramColumn='@paramName' WHERE conditionColumn='@conditionName'";
            string whereCond = " where ";

            foreach (var col in curTable.PrimaryKeys)
            {
                whereCond += col + " = @" + col + " and ";
            }
            whereCond = whereCond.Substring(0, whereCond.Length - 4);
            deleteCommandStr += whereCond;

            deleteTxt += @"
            sqlComm.CommandText =";
            deleteTxt += "\"" + deleteCommandStr + "\";\r\n";
            deleteTxt += @"
            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }";

            return deleteTxt;
        }

        public string GetFetchScript(TableSchema curTable)
        {
            string fetchTxt = "";
            fetchTxt +=
@"
        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<";

            fetchTxt += curTable.TableName+@">().Where(e => ";

            string whereCond = " ";

            foreach (var col in curTable.PrimaryKeys)
            {
                whereCond += "e."+col + " == " + col + " && ";
            }
            whereCond = whereCond.Substring(0, whereCond.Length - 3);

            fetchTxt += whereCond;
            fetchTxt += @").FirstOrDefault();
            }
        }";
            return fetchTxt;
        }

        public string GetCompareScript(TableSchema curTable)
        {
            string compareTxt = "";
            compareTxt +=
@"
        public override bool Compare(";

            compareTxt += @"Entity currentEntity)
        {
            ";
            compareTxt += curTable.TableName + " curEntity = currentEntity as " + curTable.TableName + ";\r\n";
            compareTxt += @"
            if(";

            string whereCond = " ";

            foreach (var col in curTable.PrimaryKeys)
            {
                whereCond += "curEntity." + col + " == " + col + " && ";
            }
            whereCond = whereCond.Substring(0, whereCond.Length - 3);

            compareTxt += whereCond;
            compareTxt += @")
            {
                return true;
            }
            else
            {
                return false;
            }
        }";
            return compareTxt;
        }
    }
}
