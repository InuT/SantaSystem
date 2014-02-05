using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataPlatform
{
    public class SQLServer : DataPlatform
    {
        private SqlConnection dbConnection;

        private SqlCommand dbCommand;

        private Dictionary<string, SqlDataAdapter> DataAdapters;

        public SQLServer()
        {
            dbConnection = new SqlConnection();

            dbCommand = new SqlCommand();

            data = new DataSet();

            DataAdapters = new Dictionary<string, SqlDataAdapter>();
        }

        public override DbConnection DbConnection
        {
            get{return dbConnection;}

            set{dbConnection = (SqlConnection)value;}
        }

        public override DbCommand DbCommand
        {
            get{return dbCommand;}

            set{dbCommand = (SqlCommand)value;}
        }

        public override DataSet Data
        {
            get { return data; }

            set { data = value; }
        }

        public override void ConnectionOpen()
        {
            dbConnection.Open();
        }

        public override void ConnectionClose()
        {
            dbConnection.Close();
        }

        public override void FillData(string query, string tableName)
        {
            if (DataAdapters.ContainsKey(tableName)) 
                throw new ArgumentException(tableName + ": already exists in DataAdapters :)");

            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, dbConnection);

            dataAdapter.Fill(data, tableName);

            DataAdapters.Add(tableName, dataAdapter);
        }
    }
}

