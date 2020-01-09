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
            //string myGuid = client.addQueueDocument(@"C:\Users\bweber\Pictures\amie.png", "CHARISSA", "EVANS" , 819984, "Testing from VStudio");
            string value = client.GetTravelerDocument(10198, "41c6ad30-7bac-4a30-90c8-04b6ca02e0c8");
            //Console.WriteLine("My GUID is: " + myGuid);
        }
    }
}
