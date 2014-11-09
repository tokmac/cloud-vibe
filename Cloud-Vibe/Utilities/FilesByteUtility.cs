using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Utilities
{
    public static class FilesByteUtility
    {
        public static byte[] HttpPostedFileToByteArray(HttpPostedFileBase inputFile) 
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = inputFile.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            } 
        }
        public static byte[] FileFromPathToByteArray(string path)
        {
            byte[] result;

            FileStream fileStream = new FileStream(path, FileMode.Open);
            try
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    result = ms.ToArray();
                } 
            }
            finally
            {
                fileStream.Close();
            }
            return result;
        }
    }
}