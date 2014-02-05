using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DataPlatform
{
    public class MariaDB : DataPlatform
    {
        public MariaDB()
        {
            // Santa may like MariaDB :D
        }

        public override DbConnection DbConnection
        {
            get;
            set;
        }

        public override DbCommand DbCommand
        {
            get;
            set;
        }

        public override DataSet Data
        {
            get;
            set;
        }

        public override void ConnectionOpen()
        {
            
        }

        public override void ConnectionClose()
        {
            
        }

        public override void FillData(string query, string tableName)
        {
            
        }
    }
}
