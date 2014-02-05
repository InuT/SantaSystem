using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPlatform
{
    public class DataPlatformGenerator
    {
        public enum DataPlatformType
        {
            MariaDB,
            SQLServer,
            VoltDB,
        }

        public static DataPlatform GenerateDataPlatform(DataPlatformType dataPlatformType)
        {
            switch (dataPlatformType)
            {
                case DataPlatformType.MariaDB:
                    return new MariaDB();

                case DataPlatformType.SQLServer:
                    return new SQLServer();

                case DataPlatformType.VoltDB:
                    return new VoltDB();
                
                default:
                    return null;
            }
        }
    }
}
