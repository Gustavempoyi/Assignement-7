using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static DiningPhilosophers.Sticks;

namespace DiningPhilosophers
{
    class Philosopher
    {
        private const int STEPS = 20;

        private Sticks obj = new Sticks ();

        private Form form;

        public Philosopher( Sticks obj , Form form)
        {
            this.percentageTakenLeft = 0;
            this.percentageTakenRight = 0;
            this.rightStick = right;
            this.leftStick = left;
            this.form = form;
        }

        public enum state { Thinking, Hungry, Eating, LeftStickTaken, RightStickTaken };
        private state philstate;
        public state State
        {
            get
            {
                return philstate;
            }
            set
            {
                philstate = value;
                form.Invalidate();
            }
        }

        private double percentageTakenLeft;
        public double PercentageTakenLeft
        {
            get
            {
                return percentageTakenLeft;
            }
            set
            {
                percentageTakenLeft = value;
                form.Invalidate();
            }
        }

        private double percentageTakenRight;
        private object leftStick;
        private object rightStick;
        private object right;
        private object left;

        public double PercentageTakenRight
        {
            get
            {
                return percentageTakenRight;
            }
            set
            {
                percentageTakenRight = value;
                form.Invalidate();
            }
        }

        public void Run()
        {
            while (true)
            {
                // Think for a few seconds
                State = state.Thinking;
                Thread.Sleep(2000);

                // Stop thinking, get hungry
                State = state.Hungry;

                // Take left stick
                leftStick.WaitOne();
                State = state.LeftStickTaken;
                TakeLeftStick();

                // Take right stick
                rightStick.WaitOne();
                State = state.Eating;
                TakeRightStick();

                // Eat for a few seconds
                Thread.Sleep(2000);

                // Return left stick
                ReturnLeftStick();
                leftStick.Release();

                // Return right stick
                ReturnRightStick();
                rightStick.Release();
            }
        }

        private void ReturnLeftStick()
        {
            throw new NotImplementedException();
        }

        private void ReturnRightStick()
        {
            throw new NotImplementedException();
        }

        void TakeLeftStick()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenLeft += 1.0 / STEPS;
            }
        }

        void TakeRightStick()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenRight += 1.0 / STEPS;
            }
        }
       void TakeBothSticks()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenLeft += 1.0 / STEPS;
                PercentageTakenRight += 1.0 / STEPS;
            }
        }

        void ReturnBothSticks()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenLeft -= 1.0 / STEPS;
                PercentageTakenRight -= 1.0 / STEPS;
            }

       
        }
    }
    
        
}

