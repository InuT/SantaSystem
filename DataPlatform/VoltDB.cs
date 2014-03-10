using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DataPlatform
{
    public class VoltDB : DataPlatform
    {
        public VoltDB()
        {
            // Santa may like VoltDB :D 
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
            // Under construction :D
        }

        public override void ConnectionClose()
        {
            // Under construction :D
        }

        public override void FillData(string query, string tableName)
        {
            // Under construction :D
        }

        public override void ReflectChangesInDataSet()
        {
            // Under construction :D
        }

        public override DataAdapter GetDataAdapter(string tableName)
        {
            // Under construction :D
            return (DataAdapter)new Object();
        }
    }
}
