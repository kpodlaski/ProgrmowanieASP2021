using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramowanieASPNET_2021.Poczta
{
    class Client
    {
        public readonly int ID;
        static int last_id = 0;
        PostOffice postOffice;

        public Client(PostOffice po)
        {
            this.postOffice = po;
            ID = last_id++;
        }

        public void goToPost() {
            Console.WriteLine("Client " + ID + " enter the PostOffice");
            postOffice.serveMe(this);
            Console.WriteLine("Client " + ID + " leaves the PostOffice");
        }

    }
}
