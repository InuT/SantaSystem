using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ServiceModel;

namespace SantaSystem
{
    [Serializable]
    public class Santa
    {
        private int santaID;

        private bool usingCPU;
        
        private string presentName;

        private string presentDirPath;

        public Santa(int agebtID)
        {
            this.santaID = agebtID;

            this.usingCPU = false;
        }

        public int SantaID
        {
            get{return this.santaID;}
            
            set{this.santaID = value;}
        }

        public bool UsingCPU
        {
            get{return this.usingCPU;}
            
            set{this.usingCPU = value;}
        }

        public void Work()
        {
            Console.WriteLine(string.Format("[Prototype] Santa{0}: Present For You :D", SantaID));

            ExecutePresentAssembly();
        }

        public void ExecutePresentAssembly()
        {
            string presentAssemblyPath = Path.Combine(presentDirPath, presentName);

            Assembly presentAssembly = Assembly.LoadFrom(presentAssemblyPath);

            Type[] assemblyType = presentAssembly.GetTypes();

            // Only Work() metohd :D 
            MethodInfo method = assemblyType[0].GetMethod("Work", BindingFlags.NonPublic | BindingFlags.Instance);

            var instance = Activator.CreateInstance(assemblyType[0]);

            method.Invoke(instance, null);
        }

        public void Migration(string homeURL, string endPoint, string assemblyName)
        {
            presentName = assemblyName;

            EndpointAddress endpointAddress = new EndpointAddress(string.Format("net.{0}{1}", homeURL, endPoint));

            ChannelFactory<IHome> factory = new ChannelFactory<IHome>(new NetTcpBinding());

            IHome home = factory.CreateChannel(endpointAddress);

            SendPresentAssembly(home, presentName);

            presentDirPath = home.GetPresentDirPath();

            home.HostSanta(this, presentName);
        }

        private void SendPresentAssembly(IHome home, string assemblyName)
        {
            Assembly presentAssembly = Assembly.LoadFrom(assemblyName);
            
            byte[] assemblyBytes = PutPresentAssemblyIntoSack(presentAssembly);

            home.DeployPresentAssembly(assemblyName, assemblyBytes);
        }

        private byte[] PutPresentAssemblyIntoSack(Assembly assembly)
        {
            byte[] assemblyBytes = null;

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

