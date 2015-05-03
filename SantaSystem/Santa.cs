using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Xml;

namespace SantaSystem
{
    [Serializable]
    public class Santa
    {
        private static int counter = 0;

        public Santa(string santaID, bool isSerialProcessing)
        {
            this.SantaID = santaID;
            this.IsSerialProcessing = isSerialProcessing;
            this.MaxDop = 0;
            this.NumberOfPresents = 0;
        }

        public string SantaID
        {
            get;
            set;
        }

        public bool IsSerialProcessing
        {
            get;
            set;
        }

        public int MaxDop
        {
            get;
            set;
        }

        public string PresentDirPath
        {
            get;
            set;
        }

        public string[] PresentNames
        {
            get;
            set;
        }

        public int NumberOfPresents
        {
            get;
            set;
        }

        public Dictionary<int, int> HierarchyInfo
        {
            get;
            set;
        }

        public void Work()
        {
            Console.WriteLine(string.Format("[Prototype] Santa{0}: Present For You :D", SantaID));
            ExecutePresentAssembly();
        }

        public void ExecutePresentAssembly()
        {
            var presentAssembly = Assembly.LoadFrom(Path.Combine(PresentDirPath, PresentNames[counter++]));
            Type[] presentAssemblyType = presentAssembly.GetTypes();

            // Only Work() metohd :D 
            MethodInfo method = presentAssemblyType[0].GetMethod("Work", BindingFlags.NonPublic | BindingFlags.Instance);
            var instance = Activator.CreateInstance(presentAssemblyType[0]);
            method.Invoke(instance, null);
        }

        public void Migration(string homeURL, string endPoint, string[][] presentAssemblyNames)
        {
            int presentIndex = 0;
            int hierarchyIndex = 0;

            foreach (string[] ans in presentAssemblyNames)
                NumberOfPresents = NumberOfPresents + ans.Length;

            PresentNames = new string[NumberOfPresents];
            HierarchyInfo = new Dictionary<int,int>();

            foreach (string[] ans in presentAssemblyNames)
            {
                HierarchyInfo[hierarchyIndex++] = ans.Length;

                foreach (string an in ans)
                    PresentNames[presentIndex++] = an;
            }

            var factory = new ChannelFactory<IHome>(new NetTcpBinding());
            var endpointAddress = new EndpointAddress(string.Format("net.{0}{1}", homeURL, endPoint));
            
            IHome home = factory.CreateChannel(endpointAddress);
            SendPresentAssembly(home, PresentNames);
            PresentDirPath = home.GetChristmasStockingPath();
            home.HostSanta(this);
        }

        private void SendPresentAssembly(IHome home, string[] presentAssemblyNames)
        {
            foreach (string an in presentAssemblyNames)
            {
                home.DeployPresentAssembly(an, LoadPresentAssemblyBytes(Assembly.LoadFrom(an)));
            }
        }

        private byte[] LoadPresentAssemblyBytes(Assembly presentAssembly)
        {
            byte[] bytesOfPresentAssembly;

            using (FileStream fs = File.OpenRead(presentAssembly.Location))
            {
                bytesOfPresentAssembly = new byte[fs.Length];
                fs.Read(bytesOfPresentAssembly, 0, bytesOfPresentAssembly.Length);
                fs.Close();
            }

            return bytesOfPresentAssembly;
        }
    }
}

