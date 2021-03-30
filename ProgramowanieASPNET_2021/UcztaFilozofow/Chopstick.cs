using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ProgramowanieASPNET_2021.UcztaFilozofow
{
    public class Chopstick
    {
        public int  ID { get; private set; }
        private static int max_id = 0;
        public Mutex Mutex;
        private Philosopher philosopher;

        public Chopstick()
        {
            ID = max_id++;
            Mutex = new Mutex();
        }

        public void PickUp(Philosopher p)
        {
            Mutex.WaitOne();
            philosopher = p;
            Console.WriteLine("Plilosopher " + philosopher + " picked up chopstick " + ID);
        }

        public bool PickUpV2(Philosopher p)
        {
            bool success = Mutex.WaitOne(30);
            if (success)
            {
                philosopher = p;
                Console.WriteLine("Plilosopher " + philosopher + " picked up chopstick " + ID);
            }
            return success;
        }

        public void PutDown()
        {
            Console.WriteLine("Plilosopher " + philosopher + " put down chopstick " + ID);
            philosopher = null;
            Mutex.ReleaseMutex();
        }

        public override string ToString()
        {
            return ""+ID;
        }
    }
}
