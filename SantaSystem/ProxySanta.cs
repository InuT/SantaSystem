using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SantaSystem
{
    public class ProxySanta : MarshalByRefObject
    {
        public void PresentFromSanta(Santa santa)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Present), santa);
        }

        private void Present(object stateInfo)
        {
            Santa santa = (Santa)stateInfo;

            santa.Work();
        }
    }
}
