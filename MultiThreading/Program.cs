using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    class Program
    {
        static void Main(string[] args)
        {           
            FileWriter fileWriter = new WriteWithLockStopwatch();
            fileWriter.WriteToFile(threadTimeLive: 20, threadsCount: 20);
        }
    }
}
