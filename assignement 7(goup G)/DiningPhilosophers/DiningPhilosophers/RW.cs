using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DiningPhilosophers
{
    public class RW
    {
        
        private Sticks obj = new Sticks();

        public int WritersInCS { get; private set; }
        public int ReadersInCS { get; private set; }

        public void EnterReader()
        {

            lock (obj)
            {
                while (WritersInCS == 1)
                    Monitor.Wait(obj);
                ReadersInCS++;
            }
        }

        public void ExitReader()
        {
            lock (obj)
            {
                ReadersInCS--;
                if (ReadersInCS == 0)
                    Monitor.Pulse(obj);
            }

        }
        public void EnterWriter()
        {

            lock (obj)
            {
                while (WritersInCS == 1 ||
                                   ReadersInCS > 0)
                    Monitor.Wait(obj);
                WritersInCS++;
            }

        }
        public void ExitWriter()
        {

            lock (obj)
            {
                WritersInCS--;
                Monitor.PulseAll(obj);
            }

        }
    }

}