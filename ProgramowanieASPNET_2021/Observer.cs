using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramowanieASPNET_2021
{
    class Observer
    {
        Counter c;

        public Observer(Counter c)
        {
            this.c = c;
        }

        public void observe()
        {
            while (true)
            {
                if (!c.isEven())
                {
                    Console.WriteLine("Stan:" + c.state);
                    throw new SystemException("Stan:"+c.state);
                }
            }
        }

        public void observe2()
        {
            int s;
            while (true)
            {
                s = c.value();
                if (s % 2 != 0)
                {
                    Console.WriteLine("Stan:" + s);
                    Console.WriteLine("Stan:" + c.state);
                    throw new SystemException("Stan:" + c.state);
                }
            }
        }
    }
}
