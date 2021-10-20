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
            //string value = client.GetTravelerDocument(10198, "41c6ad30-7bac-4a30-90c8-04b6ca02e0c8", "C:\\temp\\");
            //value = client.GetTravelerDocument(987354, "e6945f4b-f9ea-44a4-81f2-abac98d9ee72", "C:\\temp\\");
            string connectionString = "Server=LAS-ODSEDW-P11;Database=AMHDB;Integrated Security=true;MultipleActiveResultSets=true;";
            connectionString = "Server=AMIEDB-I01;Database=AMHDB;Integrated Security=true;";
            string sql = @"Select DISTINCT TOP 10
                tr.TravelerID,
                tri.SharePointID,
                tri.LastUpdated
                from AMHDB.[dbo].TravelerRequirement tr
                join AMHDB.[dbo].Placement p with(NoLock) on p.travelerId = tr.TravelerId
                join AMHDB.[dbo].RequirementType rt on tr.requirementTypeID = rt.requirementtypeid
                join AMHDB.[dbo].TravelerRequirementImage tri on tr.requirementid = tri.travelerrequirementID
                where
                rt.RequirementTypeID = '222'
                AND ReqTypeID = 722
                AND p.ActivityStatusID in (1, 2, 3, 6, 8, 9, 10, 11, 12)
                Order By tri.LastUpdated Desc";
            var message = client.CollectDocsFromSQL(connectionString, sql, @"F:\") ;
            Console.WriteLine(message);
            //Console.WriteLine("My GUID is: " + myGuid);
        }
    }
}
