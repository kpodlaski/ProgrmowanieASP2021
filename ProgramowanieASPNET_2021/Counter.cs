using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramowanieASPNET_2021
{
    class Counter
    {
        public volatile int state;

        internal void increase()
        {
            lock (this)
            {
                state++;
                state++;
            }
        }

        internal void counting()
        {
            while (true)
            {
                increase();
            }
        }

        internal bool isEven()
        {
            lock (this) {
                return state % 2 == 0;
            }
        }

        internal int value()
        {
            lock (this)
            {
                return state;
            }
        }
    }
}
