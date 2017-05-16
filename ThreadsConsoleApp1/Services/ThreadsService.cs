using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;


namespace ThreadsConsoleApp1.Services
{
    class ThreadsService
    {
        private int lastPrimeThread1 = 0;
        private int lastPrimeThread2 = 0;
        private int lastPrimeThread3 = 0;
        private int lastPrimeThread4 = 0;
        private int lastPrimeThread5 = 0;

        private int progress1 = 0;
        private int progress2 = 0;
        private int progress3 = 0;
        private int progress4 = 0;
        private int progress5 = 0;


        private readonly object locker1 = new object();
        private readonly object locker2 = new object();
        private readonly object locker3 = new object();
        private readonly object locker4 = new object();
        private readonly object locker5 = new object();

        private int numberOfAttempts = 400000;

        System.Timers.Timer timer;

        public ThreadsService()
        {
            RunScenerio();
        }

        public void RunScenerio()
        {
            Thread thread1 = new Thread(() => DoSomething(numberOfAttempts, ref lastPrimeThread1, ref progress1, locker1));
            thread1.Priority = ThreadPriority.Highest;

            Thread thread2 = new Thread(() => DoSomething(numberOfAttempts, ref lastPrimeThread2, ref progress2, locker2));
            thread2.Priority = ThreadPriority.AboveNormal;

            Thread thread3= new Thread(() => DoSomething(numberOfAttempts, ref lastPrimeThread3, ref progress3, locker3));
            thread3.Priority = ThreadPriority.Normal;

            Thread thread4 = new Thread(() => DoSomething(numberOfAttempts, ref lastPrimeThread4, ref progress4,  locker4));
            thread4.Priority = ThreadPriority.BelowNormal;

            Thread thread5 = new Thread(() => DoSomething(numberOfAttempts, ref lastPrimeThread5, ref progress5, locker5));
            thread5.Priority = ThreadPriority.Lowest;

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();

            timer = new System.Timers.Timer();

            timer.Interval = 2000;
            timer.Elapsed += showResult;

            timer.Start();

        } 

        private void DoSomething(int n, ref int lastPrime,ref int progress, object locker)
        {
            for (int i = 0; i <= n; i++)
            {
                if (isPrimeNumber(i))
                {
                    lock (locker)
                    {
                        lastPrime = i;
                    }
                    
                }
                lock (locker)
                {
                    progress = i;
                } 
            }
            progress++;
        }

        private void showResult(object sender, object e)
        {
            if (!isDoneWithPrimes())
            {

                Dictionary<string, int> dict = new Dictionary<string, int>();
                Console.WriteLine("Wyniki");
                lock (locker1)
                {
     
                    dict.Add("Highest", lastPrimeThread1);
                }
                lock (locker2)
                {

                    dict.Add("AboveNormal", lastPrimeThread2);
                }
                lock (locker3)
                {
                    dict.Add("Normal", lastPrimeThread3);
                }
                lock (locker4)
                {
 
                    dict.Add("BelowNormal", lastPrimeThread4);
                }
                lock (locker5)
                {
                    dict.Add("Lowest", lastPrimeThread5);
                }

                var sortedDick = dict.OrderByDescending(x => x.Value);

                foreach (KeyValuePair<string, int> entry in sortedDick)
                {
                    Console.WriteLine("{0} : {1}", entry.Key, entry.Value);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Zadanie zakończone.");
                timer.Stop();
            }
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

        private bool isDoneWithPrimes()
        {
            lock (locker1)
            {
                if (progress1 < numberOfAttempts)
                {
                    return false;
                }
            }
            lock (locker2)
            {
                if (progress2 < numberOfAttempts)
                {
                    return false;
                }
            }
            lock (locker3)
            {
                if (progress3 < numberOfAttempts)
                {
                    return false;
                }
            }
            lock (locker4)
            {
                if (progress4 < numberOfAttempts)
                {
                    return false;
                }
            }
            lock (locker5)
            {
                if (progress5 < numberOfAttempts)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
