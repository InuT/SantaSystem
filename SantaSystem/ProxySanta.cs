using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SantaSystem
{
    public class ProxySanta : MarshalByRefObject
    {
        public void PresentFromSanta(Santa santa)
        {
            if (santa.IsSerialProcessing == true)
            {
                SerialProcess(santa);
            }
            else
            {
                HybridProcess(santa);
            }
        }

        // Santa avoid creating wasteful threads in the thread pool, 
        // so don't use Start() and Wait() :D
        private void SerialProcess(Santa santa)
        {
            Task task = null;

            for (int i = 0; i < santa.NumberOfPresents; i++)
            {
                if (i == 0)
                {
                    task = Task.Run(() => Present(santa));
                }

                if (i == santa.NumberOfPresents - 1)
                {
                    break;
                }

                task.ContinueWith(tempTask => Present(santa));
            }
        }

        private void HybridProcess(Santa santa)
        {
            ParallelOptions parallelOptions = new ParallelOptions();

            parallelOptions.MaxDegreeOfParallelism = santa.MaxDop;

            Dictionary<int, int> hierarchy = santa.HierarchyInfo;

            for (int h = 0; h < hierarchy.Count; h++)
            {
                try
                {
                    Parallel.For(0, hierarchy[h], parallelOptions, i => Present(santa));
                }
                catch (AggregateException ae)
                {
                    Console.WriteLine(ae.ToString());

                    throw;
                }
            }
        }

        private void ParallelProcess(Santa santa)
        {
            Dictionary<int, int> temp = santa.HierarchyInfo;
            for (int h = 0; h < temp.Count; h++)
            {
                for (int i = 0; i < temp[h]; i++)
                {
                    Task.Run(() => Present(santa));
                }
            }
        }

        private void Present(object stateInfo)
        {
            Santa santa = (Santa)stateInfo;

            santa.Work();
        }
    }
}
