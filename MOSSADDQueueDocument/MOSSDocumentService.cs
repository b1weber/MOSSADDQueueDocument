using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace MOSSADDQueueDocument
{
    public class MOSS
    {
        public string addQueueDocument(string fileName, string travelerName, int travelerId, string Comments )
        {
            System.IO.File.WriteAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\resultContent.txt", "calling addQueueDocument.  fileName=" + fileName + "  traveleName=" + travelerName + Environment.NewLine);
            //parse first and last from travlername
            string travelerFirst = travelerName;
            string travelerLast = travelerName;
            ICEDocument.DocumentServiceClient mossClient = new ICEDocument.DocumentServiceClient("BasicHttpBinding_IDocumentService");
            System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\resultContent.txt", "mossClient acquired");
            ICEDocument.QueueDocumentItem item = new ICEDocument.QueueDocumentItem
            {
                Status = ICEDocument.QueueDocumentStatus.New,
                Priority = ICEDocument.QueueDocumentPriority.Normal,
                Title = fileName,
                Name = fileName,
                TravelerFirstName = travelerFirst,
                TravelerLastName = travelerLast,
                Comments = Comments,
                TravelerID = travelerId,
                CreateDate = DateTime.Now,
                Library = ICEDocument.QueueLibrary.Queue,
                
            };
            //System.IO.File.WriteAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\resultContent.txt", "QueueDocumentItem created");
            System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\resultContent.txt", "QueueDocumentItem created" + Environment.NewLine);
            item.Data = GetFileInBytes(fileName);
            System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\resultContent.txt", "item.Data populated, calling AddQueueDocument" + Environment.NewLine);
            System.Guid guid = mossClient.AddQueueDocument(item, item.UniqueID, "AMIE"); //Guid.Empty
            System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\resultContent.txt", "AddQueueDocument" + Environment.NewLine);

            return guid.ToString();
        }
    private byte[] GetFileInBytes(string fileName)
        {
            var fInfo = new FileInfo( fileName );
            var numBytes = fInfo.Length;
            var fs = new FileStream( fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var br = new BinaryReader(fs);
            var data = br.ReadBytes((int)numBytes);
            br.Close();
            fs.Close();
            
            return data;
        }

        
    }
}
