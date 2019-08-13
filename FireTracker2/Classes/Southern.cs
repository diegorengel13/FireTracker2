using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker2.Classes
{
    class Southern : ICommunicate
    {
        public void SayHello()
        {
            Console.WriteLine("Hello from the South");
        }
        public void SayGoodBye()
        {
            Console.WriteLine("Adios from the South");
        }

        public string Talk()
        {
            throw new NotImplementedException();
        }

        public string sing()
        {
            throw new NotImplementedException();
        }
    }
}