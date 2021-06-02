using System;
using System.Threading;

namespace ConsoleApp5
{

    class SharedRes
    {
        public static int Count;
        public static Mutex mutex = new Mutex();
    }

    class Thread1
    {
        int num;
        public Thread Thrd;

        public Thread1(string name, int n)
        {
            Thrd = new Thread(this.Run);
            num = n;
            Thrd.Name = name;
            Thrd.Start();
        }

        void Run()
        {
            Console.WriteLine(Thrd.Name + " wait for mutex");

            SharedRes.mutex.WaitOne();

            Console.WriteLine(Thrd.Name + " bring the mutex");

            do
            {
                Thread.Sleep(500);
                SharedRes.Count++;
                Console.WriteLine("in thread {0}, Count={1}", Thrd.Name, SharedRes.Count);
                num--;
            } while (num > 0);

            Console.WriteLine(Thrd.Name + " relieve the mutex ");

            SharedRes.mutex.ReleaseMutex();
        }
    }

    class Thread2
    {
        int num;
        public Thread Thrd;

        public Thread2(string name, int n)
        {
            Thrd = new Thread(new ThreadStart(this.Run));
            num = n;
            Thrd.Name = name;
            Thrd.Start();
        }

        void Run()
        {
            Console.WriteLine(Thrd.Name + " wait for mutex");
          
            SharedRes.mutex.WaitOne();

            Console.WriteLine(Thrd.Name + " bring the mutex");

            do
            {
                Thread.Sleep(500);
                SharedRes.Count--;
                Console.WriteLine("in thread {0}, Count={1}", Thrd.Name, SharedRes.Count);
                num--;
            } while (num > 0);

            Console.WriteLine(Thrd.Name + " relieve the mutex");

            SharedRes.mutex.ReleaseMutex();
        }
    }

    class Program
    {
        static void Main()
        {
            Thread1 mt1 = new Thread1("Thread1", 5);
            Thread.Sleep(1);

            Thread2 mt2 = new Thread2("Thread2", 5);

            mt1.Thrd.Join();
            mt2.Thrd.Join();

            Console.ReadLine();
        }
    }
}

