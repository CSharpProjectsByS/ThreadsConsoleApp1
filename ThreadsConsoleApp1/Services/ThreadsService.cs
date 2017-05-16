using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsConsoleApp1.Services
{
    class ThreadsService
    {
        public ThreadsService()
        {
            RunScenerio();
        }

        public void RunScenerio()
        {
            int n = 1000000;
            Thread thread1 = new Thread(() => DoSomething(n, "Watek1"));
            thread1.Priority = ThreadPriority.Highest;

            Thread thread2 = new Thread(() => DoSomething(n, "Watek2"));
            thread2.Priority = ThreadPriority.AboveNormal;

            Thread thread3= new Thread(() => DoSomething(n, "Watek3"));
            thread3.Priority = ThreadPriority.Normal;

            Thread thread4 = new Thread(() => DoSomething(n, "Watek4"));
            thread4.Priority = ThreadPriority.BelowNormal;

            Thread thread5 = new Thread(() => DoSomething(n, "Watek5"));
            thread5.Priority = ThreadPriority.Lowest;

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();

        }

        private void DoSomething(int n, String name)
        {
            Stopwatch sw = new Stopwatch();
            int lastPrime = 0;

            sw.Start();
            for (int i = 0; i <= n; i++)
            {
                if (isPrimeNumber(i))
                {
                    lastPrime = i;
                }

                TimeSpan time = sw.Elapsed;

                if (time.Seconds % 2 == 0 && time.Seconds >= 2)
                {
                    Console.WriteLine(name + ": " + lastPrime);
                }
                
                
            
            }
            sw.Stop();
        }

        private bool isPrimeNumber(int number)
        {
            for (int i = 2; i < number / 2; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
