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
            // Santa avoid creating wasteful threads in the thread pool, 
            // so don't use Start() and Wait() :D
            if (santa.IsSerialProcessing == true)
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
            else
            {
                for (int i = 0; i < santa.NumberOfPresents; i++)
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
