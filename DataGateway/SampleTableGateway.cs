using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using DataPlatform;

namespace DataGateway
{
    public class SampleTableGateway : SQLServerGateway
    {
        public override string TableName
        {
            get { return "Sample"; }
        }
    }
}
