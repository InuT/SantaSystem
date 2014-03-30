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
                ProcessSerially(santa);
            }
            else
            {
                ProcessParallelly(santa);
            }
        }

        // Santa avoid creating wasteful threads in the thread pool, 
        // so don't use Start() and Wait() :D
        private void ProcessSerially(Santa santa)
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

        private void ProcessParallelly(Santa santa)
        {
            if (santa.MaxDop == 0)
            {
                for (int i = 0; i < santa.NumberOfPresents; i++)
                {
                    Task.Run(() => Present(santa));
                }
            }
            else
            {
                ParallelOptions parallelOptions = new ParallelOptions();

                parallelOptions.MaxDegreeOfParallelism = santa.MaxDop;

                try
                {
                    Parallel.For(0, santa.NumberOfPresents, parallelOptions, i => Present(santa));
                }
                catch (AggregateException ae)
                {
                    Console.WriteLine(ae.ToString());

                    throw;
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
