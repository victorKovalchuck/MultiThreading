using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    public abstract class FileWriter
    {
        protected FileStream FileStreamWriter;
       
        protected void OpenConnection()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo != null)
            {
                var path = Path.Combine(directoryInfo.FullName, "FileToWrite.txt");

                if (!File.Exists(path))
                {
                    FileStreamWriter = new FileStream(path,
                        FileMode.Create);
                }
                else
                {
                    FileStreamWriter = new FileStream(path,
                        FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }
            }
            else
            {
                throw new IOException("Current directory does not exist");
            }
        }

        protected void CloseConnection()
        {
           FileStreamWriter.Dispose();
        }

        public abstract void WriteToFile(int threadTimeLive,int threadsCount);
        public abstract void CreateFlows(int threadTimeLive);
    }
}
