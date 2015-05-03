using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.ServiceModel;

namespace SantaSystem
{    
    public class Home : MarshalByRefObject, IHome
    {
        private static Dictionary<string, AppDomain> workSpaces;
        private static string christmasStockingPath;
        private static string homeURL;

        public static void Initialize(int port, string endPoint, string deploymentDirPath)
        {
            workSpaces = new Dictionary<string, AppDomain>();
            christmasStockingPath = deploymentDirPath;
            homeURL = string.Format("net.tcp://{0}:{1}", Environment.MachineName, port);

            var serviceHost = new ServiceHost(typeof(Home), new Uri(homeURL));
            serviceHost.AddServiceEndpoint(typeof(IHome), new NetTcpBinding(), endPoint);
            serviceHost.Open();
        }
        
        public void HostSanta(Santa santa)
        {
            try
            {
                AppDomain workSpace = null;
                
                lock (workSpaces)
                {
                    if (workSpaces.ContainsKey(santa.SantaID))
                        workSpace = workSpaces[santa.SantaID];
                    else
                        workSpace = CreateWorkSpace(santa.SantaID);
                }

                ProxySanta proxySanta = (ProxySanta)workSpace.
                                        CreateInstanceAndUnwrap(typeof(ProxySanta).Assembly.
                                        FullName, typeof(ProxySanta).FullName);

                proxySanta.PresentFromSanta(santa);
            }
            catch (Exception ex)
            {
                // TODO: error handling :D
                throw (ex);
            }
        }

        private AppDomain CreateWorkSpace(string presentAssemblyName)
        {
            var workSpace = AppDomain.CreateDomain(presentAssemblyName);
            workSpaces.Add(presentAssemblyName, workSpace);
            return workSpace;
        }

        public void DeployPresentAssembly(string presentAssemblyName, byte[] presentAssemblyBytes)
        {
            if (!Directory.Exists(christmasStockingPath))
                Directory.CreateDirectory(christmasStockingPath);

            string deploymentPath = Path.Combine(christmasStockingPath, presentAssemblyName);
            if (!File.Exists(deploymentPath))
            {
                using (FileStream fs = File.OpenWrite(deploymentPath))
                {
                    fs.Write(presentAssemblyBytes, 0, presentAssemblyBytes.Length);
                    fs.Close();
                }
            }
        }

        public string GetChristmasStockingPath()
        {
            return christmasStockingPath;
        }
    }
}
