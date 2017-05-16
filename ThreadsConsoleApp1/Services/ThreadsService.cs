using System;
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

        private int numberOfAttempts = 1000000;

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
            Console.WriteLine("Jestem w wątku");
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
            Console.WriteLine("Jestem w show Result");
            if (!isDoneWithPrimes())
            {
                Console.WriteLine("Wyniki");
                lock (locker1)
                {
                    Console.WriteLine("Highest: {0}", lastPrimeThread1);
                }
                lock (locker2)
                {
                    Console.WriteLine("AboveNormal: {0}", lastPrimeThread2);
                }
                lock (locker3)
                {
                    Console.WriteLine("Normal: {0}", lastPrimeThread3);
                }
                lock (locker4)
                {
                    Console.WriteLine("BelowNormal: {0}", lastPrimeThread4);
                }
                lock (locker5)
                {
                    Console.WriteLine("Lowest: {0}", lastPrimeThread5);
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
            Console.WriteLine("Jestem w isDoneWIthPrimes");
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
            Console.WriteLine("To jeszcze nie konieć z primamai");

            return true;
        }
    }
}
