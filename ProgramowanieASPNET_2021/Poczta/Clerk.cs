using System;
using System.Threading;

namespace ProgramowanieASPNET_2021.Poczta
{
    class Clerk
    {
        private PostOffice postOffice;
        public readonly char ID;
        private static char last_id = 'A';
        private Random rand = new Random();
        private volatile bool reserved = false;

        public Clerk(PostOffice postOffice)
        {
            this.postOffice = postOffice;
            ID = last_id++;
        }

        public void serveClient(Client client)
        {
            //można dodać lock'a ale nie jest tu potrzebny, rezerved rozwiązuje problem.
            Console.WriteLine("Urzędnik " + ID + " obsługuje klienta " + client.ID);
            Thread.Sleep(rand.Next(30, 100));
            Console.WriteLine("Urzędnik " + ID + " jest wolny");
        }

        public bool reserve()
        {
            lock(this){
                if (reserved) return false;
                reserved = true;
                return reserved;
            }
        }

        public void makeFree()
        {
            lock (this)
            {
                if (!reserved)
                {
                    Console.WriteLine("ERROR!!!, Urzędnik " + ID + " ZŁY STATUS");
                    throw new SystemException();
                }
                reserved = false;
            }
        }

    }
}