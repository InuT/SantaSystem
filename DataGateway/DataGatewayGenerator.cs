using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGateway
{
    class DataGatewayGenerator
    {
        public enum DataGatewayType
        {
            SampleTableGateway,
        }

        public static DataGateway GenerateDataGateway(DataGatewayType dataGatewayType)
        {
            switch (dataGatewayType)
            {
                case DataGatewayType.SampleTableGateway:
                    return new SampleTableGateway();
                
                default:
                    return null;
            }
        }
    }
}
