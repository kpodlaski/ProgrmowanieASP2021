using System;
using System.Collections.Generic;
using System.Threading;

namespace ProgramowanieASPNET_2021.Wyscigi
{
    public class HorseRace
    {
        public readonly Barrier startBarrier;
        public readonly Barrier finishBarrier;
        public int distance { get; private set; }
        public int noOfHorses { get; private set; }
        private List<Horse> horses;
        private long startTime =0;

        HorseRace(int distance, String[] names)
        {
            this.distance = distance;
            this.noOfHorses = names.Length;
            startBarrier = new Barrier(noOfHorses, (b) => { 
                                                            Console.WriteLine("Start Wyścigów"); 
                                                            startTime = DateTime.Now.Ticks;
                                                          }
            );
            finishBarrier = new Barrier(noOfHorses, (b) => { showRanking(); });
            horses = new List<Horse>();
            foreach (String name in names)
            {
                Horse h = new Horse(name, this);
                horses.Add(h);
                Thread t = new Thread(new ThreadStart(h.run));
                t.Start();
            }
        }

        private void showRanking()
        {
            horses.Sort((h, h2) => h.Time.CompareTo(h2.Time));
            Console.WriteLine("==================");
            Console.WriteLine("Wyniki końcowe");
            foreach(Horse horse in horses)
            {
                Console.WriteLine(horse.Name + " " + horse.Time + "s");
            }
            Console.WriteLine("==================");
        }

        public static void Main()
        {
            int distance = 150;
            String[] names = {  "Black Arrow",  "Sparrow",  "Raven",    "Quark",  "Yellow Patch",
                                "Blue Lady",    "Ladybug",  "Fat Tom",  "Crazy Tex",   "Lighting" };
            HorseRace race = new HorseRace(distance, names);
        }
    }
}