using System;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronous_App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Main_TaskStart();
            Main_TaskStartNew(); //StartNew methods are preferable to create and schedule the tasks
            Main_TaskRun();
            ////////////Using Await()////////////
            TaskAwait();
            await TaskStartNew_Await();
            TaskWithReturnValue();
            await TaskWithReturnValueAsync();
            await GetEmplyee();
            await ContinuationTask();
            SchedulingContinuationTask();
            Console.ReadKey();
        }
        #region Tasks
        public static void Main_TaskStart()
        {
            Console.WriteLine($"Main Thread  : {Thread.CurrentThread.ManagedThreadId} Statred");
            Task task1 = new Task(PrintCounter);
            task1.Start();
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }
        public static void Main_TaskStartNew()
        {
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Statred");
            Task task1 = Task.Factory.StartNew(PrintCounter);
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }
        public static void Main_TaskRun()
        {
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Statred");
            Task task1 = Task.Run(() => { PrintCounter(); });
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }
        #endregion

        #region Task.Await()
        public static void TaskAwait()
        {
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Statred");
            Task task1 = Task.Run(() =>
            {
                PrintCounter();
            });
            task1.Wait();
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }
        public static async Task TaskStartNew_Await()
        {
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Statred");
            await Task.Factory.StartNew(() =>
            {
                PrintCounter();
            });
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }
        #endregion

        #region Task Return Value
        public static void TaskWithReturnValue()
        {
            Console.WriteLine($"Main Thread Started");

            Task<double> task1 = Task.Run(() =>
            {
                return CalculateSum(10);
            });

            Console.WriteLine($"Sum is: {task1.Result}");
            Console.WriteLine($"Main Thread Completed");
            Console.ReadKey();
        }

        public static async Task TaskWithReturnValueAsync()
        {
            Console.WriteLine($"Main Thread Started");

            var task1 = await CalculateSumAsync(10);

            Console.WriteLine($"Sum is: {task1}");
            Console.WriteLine($"Main Thread Completed");
            Console.ReadKey();
        }

        public static async Task<Employee> GetEmplyee()
        {
            Console.WriteLine($"Main Thread Started");
            return await Task<Employee>.Factory.StartNew(() =>
            {
                Employee employee = new Employee()
                {
                    ID = 101,
                    Name = "Pranaya",
                    Salary = 10000
                };

                Console.WriteLine($"ID: {employee.ID}, Name : {employee.Name}, Salary : {employee.Salary}");
                Console.WriteLine($"Main Thread Completed");
                return employee;
            });
        }

        #endregion
        #region Continuation Task
        public static async Task ContinuationTask()
        {
           var task1 = await Task.Factory.StartNew(() =>
            {
                return 12;
            }).ContinueWith(antecedent =>
            {
                return $"The Square of {antecedent.Result} is: {antecedent.Result * antecedent.Result}";
            });
            Console.WriteLine(task1);
        }

        public static void SchedulingContinuationTask()
        {
            Task<int> task = Task.Run(() =>
            {
                return 10;
            });
            task.ContinueWith((i) =>
            {
                Console.WriteLine("TasK Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled);

            task.ContinueWith((i) =>
            {
                Console.WriteLine("Task Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = task.ContinueWith((i) =>
            {
                Console.WriteLine("Task Completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            completedTask.Wait();
        }

        #endregion

        #region Public Methods
        static void PrintCounter()
        {
            Console.WriteLine($"Child Thread : {Thread.CurrentThread.ManagedThreadId} Started");
            for (int count = 1; count <= 5; count++)
            {
                Console.WriteLine($"count value: {count}");
            }
            Console.WriteLine($"Child Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }
        static double CalculateSum(int num)
        {
            double sum = 0;
            for (int count = 1; count <= num; count++)
            {
                sum += count;
            }
            return sum;
        }
        static async Task<double> CalculateSumAsync(int num)
        {
            return await Task.Factory.StartNew(() =>
            {
                double sum = 0;
                for (int count = 1; count <= 10; count++)
                {
                    sum += count;
                }
                return sum;
            });

        }
        #endregion

    }
}