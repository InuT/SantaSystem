using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SantaSystem;

namespace DepatureSpace
{
    class DepatureSpace
    {
        static void Main(string[] args)
        {
            // santa0: serial processing :D
            Santa santa0 = new Santa("0000", true);
            
            // santa0: parallel processing :D
            Santa santa1 = new Santa("0001", false);

            string[] query = new string[2];

            query[0] = "Query0001.dll";
            
            query[1] = "Query0002.dll";

            santa0.Migration("tcp://localhost:10000", "/HostSanta", query);

            santa1.Migration("tcp://localhost:10000", "/HostSanta", query);
        }
    }
}
