using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SantaSystem;
using System.Threading;

namespace SantaSystem
{
    class ArrivalSpace
    {
        static void Main(string[] args)
        {
            // Sample: port = 10000
            SantaSystem.Home.Initialize(10000, "/HostSanta", "C:/SantaSystem/Present");

            Console.Out.WriteLine("Hosting...");

            while(true) 
            { 
                Thread.Sleep(10000);

                Console.WriteLine("Hosting... :D");
            }
        }
    }
}
