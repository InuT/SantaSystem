using System;
using System.ServiceModel;

namespace SantaSystem
{
    [ServiceContract]
    public interface IHome
    {
        [OperationContract]
        void HostSanta(Santa santa);

        [OperationContract]
        void DeployPresentAssembly(string assemblyName, byte[] assemblyBits);

        [OperationContract]
        string GetChristmasStockingPath();
    }
}
