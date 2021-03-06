using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramowanieASPNET_2021.Wyscigi
{
    public class Horse
    {
        public String Name {get; private set;}
        private HorseRace race;
        static Random rand = new Random();
        private double v;
        public long Time = 0;

        public Horse(String Name, HorseRace race)
        {
            this.Name = Name;
            this.race = race;
            lock(rand){
                v = rand.NextDouble() * .9 + .1;
            }
        }

        public void run()
        {
            Console.WriteLine("Koń " + Name + " gotowy do startu");
            race.startBarrier.SignalAndWait();
            int time = 0;
            double dist = 0;
            while (dist < race.distance)
            {
                time++;
                dist += v;
                lock (rand)
                {
                    if (rand.NextDouble() > 0.8)
                    {
                        v = rand.NextDouble() * .9 + .1;
                    }
                }
            }
            //This enforce proper order, but we have addotional synchronization of elements
            //lock (race) {
                Time = DateTime.Now.Ticks;
                race.consoleQueue.Enqueue("Koń " + Name + " dotarł do mety w przy tick:\t" + Time);
            //}
            //Console.WriteLine("Koń " + Name + " dotarł do mety w przy tick:\t" + Time);
            race.finishBarrier.SignalAndWait();
            Console.WriteLine("Koń " + Name + " jedzie do stajni");
        }

    }
}
