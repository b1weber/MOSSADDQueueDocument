using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace MOSSADDQueueDocument
{
    public class MOSS
    {
        public string addQueueDocument(string pathToFile, string travelerFirstName, string travelerLastName, int travelerId, string Comments )
        {
            string fileNameDebug = "resultContent_" + travelerLastName + "_" + travelerId.ToString() + " .txt";
            System.IO.File.WriteAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\" + fileNameDebug, Environment.NewLine + "calling addQueueDocument.  fileName=" + pathToFile + "  traveleName=" + travelerLastName + Environment.NewLine);
            //parse first and last from travlername
            //string fileName = string.Empty;
            string fileName = Path.GetFileName(pathToFile);
            string extension = Path.GetExtension(pathToFile).Replace(".","");
            //string travelerFirst = "CHARISSA";
            //string travelerLast = "EVANS";
            ICEDocument.DocumentServiceClient mossClient = new ICEDocument.DocumentServiceClient("BasicHttpBinding_IDocumentService");
            System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\" + fileNameDebug, "mossClient acquired");
            ICEDocument.QueueDocumentItem item = new ICEDocument.QueueDocumentItem
            {
                Status = ICEDocument.QueueDocumentStatus.New,
                Priority = ICEDocument.QueueDocumentPriority.Normal,
                Title = fileName,
                Name = fileName,
                FileName = fileName,
                Extension = extension,
                TravelerFirstName = travelerFirstName,
                TravelerLastName = travelerLastName,
                Comments = Comments,
                TravelerID = travelerId,
                CreateDate = DateTime.Now,
                Library = ICEDocument.QueueLibrary.Queue,
                Email = "NoReply@amnheathcare.com"
            };

            item.Data = GetFileInBytes(pathToFile);
            System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\resultContent.txt", "item.Data populated, calling AddQueueDocument" + Environment.NewLine);
            System.Guid guid = System.Guid.Empty;
            try
            {
                guid = mossClient.AddQueueDocument(item, Guid.Empty, "AMIE"); 
                System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\" + fileNameDebug, guid + Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\" + fileNameDebug, Environment.NewLine + "AddQueueDocumentException Error: " + ex.Message + Environment.NewLine);
                System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\" + fileNameDebug, Environment.NewLine + "AddQueueDocumentException StackTrace: " + ex.StackTrace + Environment.NewLine);
                System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\" + fileNameDebug, "AddQueueDocumentException endpoint:" + mossClient.Endpoint.Address + Environment.NewLine);
            }
            System.IO.File.AppendAllText(@"D:\\Data\\BPMA\\FA_Worker_Robust_Files\\" + fileNameDebug, Environment.NewLine+ "AddQueueDocument" + Environment.NewLine);

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
