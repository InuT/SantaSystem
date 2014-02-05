using System;
using System.Collections;
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
        private static Dictionary<string, AppDomain> workSpaces = new Dictionary<string, AppDomain>();

        private static string homeURL;

        private static string presentDirPath;

        public static void Initialize(int port, string endPoint, string dirPath)
        {
            homeURL = string.Format("net.tcp://{0}:{1}", Environment.MachineName, port);

            presentDirPath = dirPath;

            ServiceHost serviceHost = new ServiceHost(typeof(Home), new Uri(homeURL));

            serviceHost.AddServiceEndpoint(typeof(IHome), new NetTcpBinding(), endPoint);
            
            serviceHost.Open();
        }
        
        public void HostSanta(Santa santa, string assemblyName)
        {
            try
            {
                AppDomain workSpace = null;
                
                lock (workSpaces)
                {
                    if (workSpaces.ContainsKey(assemblyName))
                    {
                        workSpace = workSpaces[assemblyName];
                    }
                    else
                    {
                        workSpace = CreateWorkSpace(assemblyName);
                    }
                }

                ProxySanta proxySanta = (ProxySanta)workSpace.
                                        CreateInstanceAndUnwrap(typeof(ProxySanta).Assembly.
                                        FullName, typeof(ProxySanta).FullName);

                proxySanta.PresentFromSanta(santa);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                
                throw;
            }
        }

        private AppDomain CreateWorkSpace(string assemblyName)
        {
            AppDomain workSpace = AppDomain.CreateDomain(assemblyName);

            workSpaces.Add(assemblyName, workSpace);

            return workSpace;
        }

        public void DeployPresentAssembly(string assemblyName, byte[] assemblyBytes)
        {
            if (!Directory.Exists(presentDirPath))
            {
                Directory.CreateDirectory(presentDirPath);
            }

            string filePath = Path.Combine(presentDirPath, assemblyName);

            if (!File.Exists(filePath))
            {
                FileStream fileStream = File.OpenWrite(filePath);

                fileStream.Write(assemblyBytes, 0, assemblyBytes.Length);

                fileStream.Close();
            }
        }

        public string GetPresentDirPath()
        {
            return presentDirPath;
        }
    }
}
