using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static DiningPhilosophers.RW;


namespace DiningPhilosophers
{
    public class Sticks
    {
        private bool[] stick = new bool[2]; // initially false, i.e. not used

        // Try to pick up the sticks with the designated numbers
        public void Get(int left, int right)
        {
            lock (this)
            {
                while (stick[left] || stick[right]) Monitor.Wait(this);
                stick[left] = true; stick[right] = true;
            }
        }

        // Lay down the sticks with the designated numbers
        public void Put(int left, int right)
        {
            lock (this)
            {
                stick[left] = false; stick[right] = false;
                Monitor.PulseAll(this);
            }
        }
    }

    internal class Monitor
    {
        internal static void Pulse(Sticks obj)
        {
            throw new NotImplementedException();
        }

        internal static void PulseAll(Sticks sticks)
        {
            throw new NotImplementedException();
        }

        internal static void Wait(Sticks sticks)
        {
            throw new NotImplementedException();
        }
    }
}
