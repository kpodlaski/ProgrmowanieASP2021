using System;
using System.Collections.Generic;
using System.Threading;

namespace ProgramowanieASPNET_2021.Poczta
{
    class PostOffice
    {
        List<Clerk> clerks = new List<Clerk>();
        Semaphore semaphore = null;

        public PostOffice(int numberOfClerks)
        {
            for (int i=0; i<numberOfClerks; i++)
            {
                clerks.Add(new Clerk(this));
            }
            semaphore = new Semaphore(0, numberOfClerks);
        } 

        internal void serveMe(Client client)
        {
            bool success = false;
            bool semaphore_taken = false;
            try
            {
                semaphore_taken = semaphore.WaitOne(10);
                Console.WriteLine("Klient " + client.ID + " szuka wolnego okienka");
                Clerk clerk = getFreeClerk();
                clerk.serveClient(client);
                clerk.makeFree();
                success = true;
                semaphore.Release();
            }
            catch (AbandonedMutexException)
            {
                success = false;
            }
            if (!success)
            {
                if (semaphore_taken) semaphore.Release();
                serveMe(client);
            }
        }

        private Clerk getFreeClerk()
        {
            lock(this){
                for(int i=0; i<clerks.Count; i++)
                {
                    if (clerks[i].reserve())
                    {
                        return clerks[i];
                    }
                }
            }
            Console.WriteLine("ERROR!!!!, BRAK WOLNYCH URZĘDNIKÓW POD SEMAFOREM ");
            throw new SystemException();
        }


        public static void Main()
        {
            int noOfClients = 1000;
            int noOfClerks = 3;
            PostOffice po = new PostOffice(noOfClerks);
            for (int i=0; i< noOfClients; i++)
            {
                Client c = new Client(po);
                Thread th = new Thread(new ThreadStart(c.goToPost));
                th.Start();
            }
            Console.WriteLine("Otwieramy wszystkie okienka");
            po.semaphore.Release(noOfClerks);

        }
    }
}