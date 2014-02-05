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
            // Sample: 3 santas presents 'Query0001.dll' :)
            Santa santa0 = new Santa(0000);

            Santa santa1 = new Santa(0001);

            Santa santa2 = new Santa(0002);

            santa0.Migration("tcp://localhost:10000", "/HostSanta", "Query0001.dll");

            santa1.Migration("tcp://localhost:10000", "/HostSanta", "Query0002.dll");

            santa2.Migration("tcp://localhost:10000", "/HostSanta", "Query0001.dll");
        }
    }
}
