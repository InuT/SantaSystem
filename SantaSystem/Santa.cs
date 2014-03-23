using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using System.ServiceModel;

namespace SantaSystem
{
    [Serializable]
    public class Santa
    {
        private string santaID;

        private bool usingCPU;

        private bool isSerialProcessing;

        private static int counter = 0;

        private string[] presentName;

        private int numberOfPresents;

        private string presentDirPath;

        public Santa(string santaID, bool isSerialProcessing)
        {
            this.santaID = santaID;

            this.isSerialProcessing = isSerialProcessing;

            this.usingCPU = false;
        }

        public string SantaID
        {
            get{return this.santaID;}
            
            set{this.santaID = value;}
        }

        public bool UsingCPU
        {
            get{return this.usingCPU;}
            
            set{this.usingCPU = value;}
        }

        public bool IsSerialProcessing
        {
            get { return this.isSerialProcessing; }

            set { this.isSerialProcessing = value; }
        }

        public int NumberOfPresents
        {
            get { return this.presentName.Length; }

            set { this.numberOfPresents = value; }
        }

        public void Work()
        {
            Console.WriteLine(string.Format("[Prototype] Santa{0}: Present For You :D", SantaID));

            ExecutePresentAssembly();
        }

        public void ExecutePresentAssembly()
        {
            string presentAssemblyPath = Path.Combine(presentDirPath, presentName[counter++]);

            Assembly presentAssembly = Assembly.LoadFrom(presentAssemblyPath);

            Type[] assemblyType = presentAssembly.GetTypes();

            // Only Work() metohd :D 
            MethodInfo method = assemblyType[0].GetMethod("Work", BindingFlags.NonPublic | BindingFlags.Instance);

            var instance = Activator.CreateInstance(assemblyType[0]);

            method.Invoke(instance, null);
        }

        public void Migration(string homeURL, string endPoint, string[] assemblyName)
        {
            int index = 0;

            presentName = new string[assemblyName.Length];

            foreach (string an in assemblyName)
            {
                presentName[index] = an;

                index++;
            }

            EndpointAddress endpointAddress = new EndpointAddress(string.Format("net.{0}{1}", homeURL, endPoint));

            ChannelFactory<IHome> factory = new ChannelFactory<IHome>(new NetTcpBinding());

            IHome home = factory.CreateChannel(endpointAddress);

            SendPresentAssembly(home, presentName);

            presentDirPath = home.GetPresentDirPath();

            home.HostSanta(this);
        }

        private void SendPresentAssembly(IHome home, string[] assemblyName)
        {
            Assembly presentAssembly;

            byte[] assemblyBytes;

            foreach (string an in assemblyName)
            {
                presentAssembly = Assembly.LoadFrom(an);

                assemblyBytes = PutPresentAssemblyIntoSack(presentAssembly);

                home.DeployPresentAssembly(an, assemblyBytes);
            }
        }

        private byte[] PutPresentAssemblyIntoSack(Assembly assembly)
        {
            byte[] assemblyBytes;

            using (FileStream fileStream = File.OpenRead(assembly.Location))
            {
                assemblyBytes = new byte[fileStream.Length];

                fileStream.Read(assemblyBytes, 0, assemblyBytes.Length);

                fileStream.Close();
            }

            return assemblyBytes;
        }
    }
}

