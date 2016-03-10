using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    public class WriteWithLock : FileWriter
    {
        private int _indexer = 0;
        private readonly object _locker = new object();

        public override void WriteToFile(int threadsCount, int threadTimeLive)
        {
            OpenConnection();
            var list = new List<Task>();

            for (int i = 0; i < threadsCount; i++)
            {
                var task = Task.Factory.StartNew(() => CreateFlows(threadTimeLive));

                list.Add(task);
            }

            Task.WaitAll(list.ToArray());
            CloseConnection();
        }

        public override void CreateFlows(int threadTimeLive)
        {
            lock (_locker)
            {
                _indexer++;
                var buffer = Encoding.ASCII.GetBytes(_indexer + "\r\n");
                FileStreamWriter.Write(buffer, 0, buffer.Length);
                FileStreamWriter.Flush(true);
                Task.Delay(threadTimeLive);               
            }
        }
    }
}
