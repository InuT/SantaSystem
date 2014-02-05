using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SantaSystem
{
    [ServiceContract]
    public interface IHome
    {
        [OperationContract]
        void HostSanta(Santa santa, string assemblyName);

        [OperationContract]
        void DeployPresentAssembly(string assemblyName, byte[] assemblyBits);

        [OperationContract]
        string GetPresentDirPath();
    }
}
