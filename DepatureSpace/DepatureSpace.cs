using System;
using SantaSystem;

namespace DepatureSpace
{
    class DepatureSpace
    {
        static void Main(string[] args)
        {
            // santa0: serial processing :D
            Santa santa0 = new Santa("0000", true);
            
            // santa0: hybrid processing :D
            Santa santa1 = new Santa("0001", false);
            santa1.MaxDop = 2;

            var query = new string[3][];
            query[0] = new string[] { "Query0002.dll", "Query0003.dll", "Query0004.dll" };
            query[1] = new string[] { "Query0001.dll" };
            query[2] = new string[] { "Query0003.dll", "Query0003.dll", "Query0003.dll", "Query0003.dll", "Query0003.dll" };

            santa0.Migration("tcp://TAKASHI-PC:10013", "/HostSanta", query);
            //santa1.Migration("tcp://localhost:10013", "/HostSanta", query);
        }
    }
}
