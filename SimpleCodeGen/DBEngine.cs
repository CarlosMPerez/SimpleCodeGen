using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SimpleCodeGen
{
    public class DBEngine
    {
        private string _connString;

        public DBEngine(string strConn)
        {
            _connString = strConn;
        }

        public List<string> GetDatabases()
        {
            List<string> resp = new List<string>();
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            DataTable dtBases = new DataTable();
            string sql = "SELECT * FROM sys.databases WHERE LEN(owner_sid) > 1 ORDER BY name";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comm;
            da.Fill(dtBases);

            foreach (DataRow row in dtBases.Rows)
            {
                resp.Add(row["name"].ToString());
            }

            conn.Close();
            return resp;
        }

        public List<string> GetTables(string databaseName)
        {
            List<string> resp = new List<string>();

            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            string sql = String.Format("SELECT table_name FROM {0}.INFORMATION_SCHEMA.TABLES " +
                "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT IN ('sysdiagrams', 'dtproperties') ORDER BY TABLE_NAME", databaseName);
            DataTable dtTables = new DataTable();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter();
            comm.CommandText = sql;
            da.SelectCommand = comm;
            da.Fill(dtTables);
            foreach (DataRow subRow in dtTables.Rows)
            {
                resp.Add(subRow["table_name"].ToString());
            }

            conn.Close();
            return resp;
        }

        public Dictionary<string, string> GetColumnsWithCSharpTypes(string db, string table)
        {
            Dictionary<string, string> resp = new Dictionary<string, string>();

            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            string sql = String.Format("SELECT COLUMN_NAME, DATA_TYPE FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{1}'",
                                        db, table);
            DataTable dtTables = new DataTable();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter();
            comm.CommandText = sql;
            da.SelectCommand = comm;
            da.Fill(dtTables);
            foreach (DataRow subRow in dtTables.Rows)
            {
                resp.Add(subRow["COLUMN_NAME"].ToString(), subRow["DATA_TYPE"].ToString());
            }

            conn.Close();
            return resp;
        }

        public string GetPrimaryKeyColumn(string db, string table)
        {
            string resp = "";
            string sql = String.Format("select CCU.COLUMN_NAME from {0}.INFORMATION_SCHEMA.TABLE_CONSTRAINTS as TC " +
                                        "inner join {0}.INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE as CCU " +
                                            "on TC.CONSTRAINT_CATALOG = CCU.CONSTRAINT_CATALOG " +
                                            "and TC.CONSTRAINT_SCHEMA = CCU.CONSTRAINT_SCHEMA " +
                                            "and TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME " +
                                        "where TC.CONSTRAINT_SCHEMA = 'dbo' " +
                                        "and TC.TABLE_NAME = '{1}' " +
                                        "and TC.CONSTRAINT_NAME LIKE 'PK_%'", db, table);

            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            DataTable dtTables = new DataTable();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter();
            comm.CommandText = sql;
            da.SelectCommand = comm;
            da.Fill(dtTables);
            if (dtTables.Rows.Count == 1) { resp = dtTables.Rows[0][0].ToString(); }
            else { resp = ""; }

            conn.Close();
            return resp;
        }
    }

}
