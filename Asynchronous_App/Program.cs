using System;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronous_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Main_TaskStartNew();
            Task task1 = Task.Factory.StartNew(PrintCounter);
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
            Console.ReadKey();
        }
        public static void Main_TaskStartNew()
        {
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Statred");
        }

        static void PrintCounter()
        {
            Console.WriteLine($"Child Thread : {Thread.CurrentThread.ManagedThreadId} Started");
            for (int count = 1; count <= 5; count++)
            {
                Console.WriteLine($"count value: {count}");
            }
            Console.WriteLine($"Child Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }
    }
}