using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SimpleCodeGen.CodeGenerator
{
    public class DBEngine
    {
        private string _connString;

        public DBEngine(string strConn)
        {
            _connString = strConn;
        }

        /// <summary>
        /// Returns a list of all Databases in the connection
        /// </summary>
        /// <returns>List of databases</returns>
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

        /// <summary>
        /// Gets a list of all tables of a given database
        /// </summary>
        /// <param name="databaseName">The database name</param>
        /// <returns>List of tables</returns>
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

        /// <summary>
        /// Gets all the columns of a database.table with its corresponding C# type
        /// </summary>
        /// <param name="db">The database</param>
        /// <param name="table">The table</param>
        /// <returns>A dictionary with the column name and its type</returns>
        public Dictionary<string, string> GetColumnsWithDataTypes(string db, string table)
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

        /// <summary>
        /// Gets the name of the primary key column of a table
        /// </summary>
        /// <param name="db">the database</param>
        /// <param name="table">the table</param>
        /// <returns>the name of the primary key column</returns>
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
