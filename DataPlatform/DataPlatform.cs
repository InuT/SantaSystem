using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DataPlatform
{
    public abstract class DataPlatform
    {
        protected DataSet data;

        public DataPlatform()
        {
        }

        public abstract DbConnection DbConnection 
        { 
            get; 
            set; 
        }

        public abstract DbCommand DbCommand 
        { 
            get; 
            set; 
        }

        public abstract DataSet Data 
        { 
            get; 
            set; 
        }

        public abstract void ConnectionOpen();

        public abstract void ConnectionClose();

        public abstract DataAdapter GetDataAdapter(string tableName);

        public abstract void FillData(string query, string tableName);

        public abstract void ReflectChangesInDataSet();
    }
}
