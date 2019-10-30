using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOSSADDQueueDocument;

namespace MossClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MOSSADDQueueDocument.MOSS client = new MOSS();
            string myGuid = client.addQueueDocument(@"C:\Users\bweber\Pictures\amie.png", "CHARISSA", "EVANS" , 819984, "Testing from VStudio");
            Console.WriteLine("My GUID is: " + myGuid);
        }
    }
}
