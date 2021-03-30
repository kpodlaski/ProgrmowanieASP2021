using System;
using System.Threading;

namespace ProgramowanieASPNET_2021.UcztaFilozofow
{
    public class Philosopher
    {
        private int ID;
        static int max_id;
        static int sleepTime = 4;
        static int eatTime = 5;
        static Random Random = new Random();
        private Chopstick leftChopstick;
        private Chopstick rightChopstick;

        public Philosopher(Chopstick lC, Chopstick rC)
        {
            ID = max_id++;
            leftChopstick = lC;
            rightChopstick = rC;
        }
        
        public void Live()
        {
            Console.WriteLine("Start Filozofa " + this);
            Console.WriteLine("Filozof " + this + " lewa:" + leftChopstick + " prawa:" + rightChopstick);
            Thread.Sleep(Random.Next()%30);
            while (true)
            {
                leftChopstick.PickUp(this);
                rightChopstick.PickUp(this);
                Console.WriteLine("Filozof " + this + " je" );
                Thread.Sleep(Random.Next() % eatTime);
                Console.WriteLine("Filozof " + this + " skończył jeść");
                rightChopstick.PutDown();
                leftChopstick.PutDown();
                Thread.Sleep(Random.Next() % sleepTime);
            }
        }

        public void Start()
        {
            Thread t = new Thread(new ThreadStart(this.Live));
            t.Start();
        }

        override public String ToString()
        {
            return "" + ID;
        }
    }
}