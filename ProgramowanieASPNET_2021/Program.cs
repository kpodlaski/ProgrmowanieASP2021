using System;
using System.Threading;

namespace ProgramowanieASPNET_2021
{
    class Program
    {
        Counter c = new Counter();
        int times;
        public void count ()
        {
            
            //Console.WriteLine("Counting ....");
            for (int i = 0; i < times; i++)
            {
                c.increase();
            }
            //Console.WriteLine("Finished Counting");
            
        }

        static void Main(string[] args)
        {
            Counter c = new Counter();
            Observer o = new Observer(c);
            Thread th_counter = new Thread(new ThreadStart(c.counting));
            th_counter.Start();
            Thread th_observer = new Thread(new ThreadStart(o.observe2));
            th_observer.Start();
        }

        static void Main2(string[] args)
        {
            Console.WriteLine("Start counting");
            Program p = new Program();
            p.times = 1000;
            int numberOfCounters = 500;
            for (int i=0; i<numberOfCounters; i++)
            {
                Thread th = new Thread(new ThreadStart(p.count));
                //p.count();
                th.Start();
            }
            Console.WriteLine("Final State" + p.c.state);
            Thread.Sleep(500);
            Console.WriteLine("Final State" + p.c.state);
            Thread.Sleep(3000);
            Console.WriteLine("Final State" + p.c.state);
        }


    }
}
