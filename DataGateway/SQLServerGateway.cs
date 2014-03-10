using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using DataPlatform;

namespace DataGateway
{
    public class SQLServerGateway : DataGateway
    {
        protected string tableName;

        public override DataSet Data
        {
            get{return dataPlatform.Data;}
        }

        public virtual string TableName 
        {
            get{return tableName;}
        }

        public SQLServerGateway()
        {
            this.tableName = TableName;

            OpenGateway();
        }

        public SQLServerGateway(SQLServer dataPlatform)
        {
            this.tableName = TableName;

            this.dataPlatform = dataPlatform;
        }

        public SQLServerGateway(SQLServer dataPlatform, string tableName)
        {
            this.tableName = null;

            this.dataPlatform = dataPlatform;
        }

        public override DataPlatform.DataPlatform Dataplatform
        {
            get { return dataPlatform; }

            set { dataPlatform = (SQLServer)value; }
        }

        public override void OpenGateway()
        {
            dataPlatform = DataPlatformGenerator.GenerateDataPlatform(DataPlatformGenerator.DataPlatformType.SQLServer);

            dataPlatform.ConnectionOpen();
        }

        public override void LoadAll()
        {
            string commandString = string.Format("SELECT * FROM {0}", TableName);

            dataPlatform.FillData(commandString, TableName);
        }

        public override void LoadWhere(string whereClause)
        {
            string commandString = string.Format("SELECT * FROM {0} where {1}", TableName, whereClause);

            dataPlatform.FillData(commandString, TableName);
        }

        public virtual void setDataAdapter()
        {
        }

        public virtual void Insert()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Delete()
        {
        }
    }
}
