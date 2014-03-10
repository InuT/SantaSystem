using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using DataPlatform;

namespace DataGateway
{
    public abstract class DataGateway
    {
        protected DataPlatform.DataPlatform dataPlatform;

        public abstract DataSet Data
        {
            get;
        }

        public DataGateway()
        {
            OpenGateway();
        }

        public DataGateway(DataPlatform.DataPlatform dataPlatform)
        {
            this.dataPlatform = dataPlatform;
        }

        public abstract DataPlatform.DataPlatform Dataplatform
        {
            get;

            set;
        }

        public abstract void OpenGateway();

        public abstract void LoadAll();

        public abstract void LoadWhere(string whereClause);
    }
}
