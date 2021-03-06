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
        public bool LeftHanded = false;

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
                Console.WriteLine("Filozof " + this + " podnosi pałeczki");
                // Poprawka wprowadzona aby program nie ulegał zakleszczeniu
                if (!LeftHanded)
                {
                    leftChopstick.PickUp(this);
                    rightChopstick.PickUp(this);
                }
                else
                {
                    rightChopstick.PickUp(this);
                    leftChopstick.PickUp(this);
                }
                Console.WriteLine("Filozof " + this + " je" );
                Thread.Sleep(Random.Next() % eatTime);
                Console.WriteLine("Filozof " + this + " skończył jeść");
                rightChopstick.PutDown();
                leftChopstick.PutDown();
                Console.WriteLine("Filozof " + this + " śpi");
                Thread.Sleep(Random.Next() % sleepTime);
                Console.WriteLine("Filozof " + this + " skończył spać");
            }
        }

        //Altenatywne rozwiązanie poprawne z czasem dla WaitOne
        public void LiveV2()
        {
            Console.WriteLine("Start Filozofa " + this);
            Console.WriteLine("Filozof " + this + " lewa:" + leftChopstick + " prawa:" + rightChopstick);
            Thread.Sleep(Random.Next() % 30);
            while (true)
            {
                Console.WriteLine("Filozof " + this + " podnosi pałeczki");
                leftChopstick.PickUpV2(this);
                bool sucess = rightChopstick.PickUpV2(this);
                if (!sucess) {
                    leftChopstick.PutDown();
                    continue;
                }
                Console.WriteLine("Filozof " + this + " je");
                Thread.Sleep(Random.Next() % eatTime);
                Console.WriteLine("Filozof " + this + " skończył jeść");
                rightChopstick.PutDown();
                leftChopstick.PutDown();
                Console.WriteLine("Filozof " + this + " śpi");
                Thread.Sleep(Random.Next() % sleepTime);
                Console.WriteLine("Filozof " + this + " skończył spać");
            }
        }

        public void Start()
        {
            //Thread t = new Thread(new ThreadStart(this.Live));
            Thread t = new Thread(new ThreadStart(this.LiveV2));
            t.Start();
        }

        override public String ToString()
        {
            return "" + ID;
        }
    }
}