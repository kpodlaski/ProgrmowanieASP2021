using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramowanieASPNET_2021.UcztaFilozofow
{
    class Feast
    {
        public static void Main()
        {
            int n = 5;
            Chopstick[] cSticks = new Chopstick[n];
            for (int i=0; i<n; i++)
            {
                cSticks[i] = new Chopstick();
            }
            Philosopher[] philosophers = new Philosopher[n];
            for (int i = 0; i < n; i++)
            {
                philosophers[i] = new Philosopher(cSticks[ (n+ i - 1)%n], cSticks[i]);
                //Poprawka wprowadzona aby program nie ulegał zakleszczeniu
                if (i == 0) philosophers[i].LeftHanded = true;
                philosophers[i].Start();
            }
        }
    }
}
