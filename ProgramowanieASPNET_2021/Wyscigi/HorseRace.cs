using System;
using System.Collections.Concurrent;
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
        public ConcurrentQueue<String> consoleQueue;
        private volatile bool raceInProgress = false;

        HorseRace(int distance, String[] names)
        {
            this.distance = distance;
            this.noOfHorses = names.Length;
            raceInProgress = true;
            startBarrier = new Barrier(noOfHorses, (b) => {                                                            
                                                            Console.WriteLine("Start Wyścigów"); 
                                                            startTime = DateTime.Now.Ticks;
                                                          }
            );
            finishBarrier = new Barrier(noOfHorses, (b) => { 
                                                            raceInProgress = false;
                                                            showRanking(); });
            horses = new List<Horse>();
            foreach (String name in names)
            {
                Horse h = new Horse(name, this);
                horses.Add(h);
                Thread t = new Thread(new ThreadStart(h.run));
                t.Start();
            }
            consoleQueue = new ConcurrentQueue<String>();
            Thread th = new Thread(new ThreadStart(consoleThread));
            th.Start();
        }

        private void showRanking()
        {
            horses.Sort((h, h2) => h.Time.CompareTo(h2.Time));
            Console.WriteLine("==================");
            Console.WriteLine("Wyniki końcowe");
            foreach (Horse horse in horses)
            {
                double time = (horse.Time - startTime)*1.0 / TimeSpan.TicksPerMillisecond;
                Console.WriteLine(horse.Name + " " + time + "s");
            }
            Console.WriteLine("==================");
        }

        private void consoleThread() {
            String line;
            while (true) 
            {
                if (consoleQueue.TryDequeue(out line)){ 
                    Console.WriteLine(line);
                }
            }
        }
        public static void Main()
        {
            int distance = 150;
            String[] names = {  "Blackie",  "Sparrow",  "Raven",    "Quark",  "Pixie",
                                "Tipsy",    "Ladybug",  "Fat Tom",  "Tex",   "Light" };
            HorseRace race = new HorseRace(distance, names);
        }
    }
}