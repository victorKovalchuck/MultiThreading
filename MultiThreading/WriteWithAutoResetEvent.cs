using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    public class WriteWithAutoResetEvent : FileWriter
    {
        private readonly AutoResetEvent _event1 = new AutoResetEvent(true);
        private int _indexer = 0;

        public override void WriteToFile(int threadTimeLive, int threadsCount)
        {
            OpenConnection();
            var list = new List<Task>();

            for (int i = 0; i < threadsCount; i++)
            {
                _event1.WaitOne();
                var task = Task.Factory.StartNew(() => CreateFlows(threadTimeLive));
                list.Add(task);

            }

            Task.WaitAll(list.ToArray());
            CloseConnection();
        }

        public override void CreateFlows(int threadTimeLive)
        {
            _indexer++;
            var buffer = Encoding.ASCII.GetBytes(_indexer + "\r\n");
            FileStreamWriter.Write(buffer, 0, buffer.Length);
            FileStreamWriter.Flush(true);
            Task.Delay(threadTimeLive); 
            _event1.Set();
        }
    }
}
