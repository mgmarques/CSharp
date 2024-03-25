using System;
using System.Threading.Tasks;

namespace AsyncExes;

public class Program
{
    public static async Task Main()
    {
        Task<int> task = DoAsyncWork();
        int result = await task;
        Console.WriteLine($"Result: {result}");

        Console.WriteLine("Main thread ID: " + Environment.CurrentManagedThreadId);

        // Using async/await with TPL
        int res = await Task.Run(() =>
        {
            Console.WriteLine("Task.Run thread ID: " + Environment.CurrentManagedThreadId);
            // Simulate a CPU-bound operation
            for (int i = 0; i < 1000000; i++)
            {
                // Perform some heavy computation
                Math.Sqrt(i);
            }
            return 42; // Return a result
        });

        Console.WriteLine("Result: " + res);

        // TPL provides methods like Task.Run and Parallel.For to easily parallelize work:
        Parallel.For(0, 10, i =>
        {
            Console.WriteLine($"Task {i} is running on thread {Task.CurrentId}");
            Task.Delay(1000).Wait();
        });

        // Continuations: Tasks can have continuations, which are tasks that start running once the original task completes.
        // This allows you to chain tasks together in a fluent manner, expressing dependencies between them.
        Task firstTask = Task.Run(() => { Console.Write("\nfirstTask"); });
        Task secondTask = firstTask.ContinueWith(prevTask => { Console.Write(" folowed by secondTask!\n"); });

        // TPL makes exception handling and cancellation straightforward:
        CancellationTokenSource cts = new CancellationTokenSource();
        Task<int> task2 = DoAsyncWorkException(cts.Token);

        try
        {
            int result2 = task2.Result;
            Console.WriteLine($"Result: {result2}");
        }
        catch (AggregateException ex)
        {
            Console.WriteLine($"Error: {ex.InnerException.Message}");
        }

        //sample code

        Console.WriteLine("Starting parallel computation...");

        // Create and start multiple tasks to perform computations in parallel
        Task<int>[] computationTasks = new Task<int>[5];
        for (int i = 0; i < computationTasks.Length; i++)
        {
            int taskId = i; // Capture the current i value
            computationTasks[i] = Task.Run(() => Compute(taskId));
        }

        // Continue with processing the results when all tasks are completed
        Task.WhenAll(computationTasks)
            .ContinueWith(completedTasks =>
            {
                Console.WriteLine("All computations completed. Processing results...");

                foreach (var task in computationTasks)
                {
                    Console.WriteLine($"Result from Task {task.Id}: {task.Result}");
                }

                Console.WriteLine("Processing completed.");
            })
            .Wait();

        Console.WriteLine("Main thread finished.");

        // Exception handling in the .NET Task Parallel Library with C#: the basics
        /* If a task throws an exception that’s not caught within the task body then
         * the exception remains hidden at first. It bubbles up again when a trigger
         * method is called, such as Task.Wait, Task.WaitAll, etc. 
         * The exception will then be wrapped in an AggregateException object which
         * has an InnerException property. This inner exception will hold the actual
         * type of exception thrown by the tasks that are waited on.
         */
        // Construct and start the tasks
        Task firstTask1 = Task.Factory.StartNew(() =>
        {
            ArgumentNullException exception = new ArgumentNullException();
            exception.Source = "First task";
            throw exception;
        });
        Task secondTask1 = Task.Factory.StartNew(() =>
        {
            throw new ArgumentException();
        });
        Task thirdTask1 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Hello from the third task.");
        });
        // Wait for all the tasks, catch the aggregate exception and analyse its inner exceptions:
        try
        {
            Task.WaitAll(firstTask1, secondTask1, thirdTask1);
        }
        catch (AggregateException ex)
        {
            foreach (Exception inner in ex.InnerExceptions)
            {
                Console.WriteLine("Exception type {0} from {1}",
                    inner.GetType(), inner.Source);
            }
        }

        // Other ContinueWith example:
        Task<int> originalTask = Task.Run(() =>
        {
            // Simulate some time-consuming computation
            return 27;
        });

        Task<string> continuationTask = originalTask.ContinueWith(previousTask =>
        {
            int result = previousTask.Result;
            return $"The answer is {result}.";
        });

        continuationTask.Wait(); // Wait for the continuation task to complete

        Console.WriteLine(continuationTask.Result); // Output: "The answer is 27."

        /*Parallel.For and Parallel.ForEach Overview:
         * Parallel.For: This construct is used to parallelize a for loop. 
         * It divides the iterations of the loop into multiple tasks that can run concurrently on different threads. 
         * It's suitable for scenarios where you have a fixed number of iterations
         * and want to distribute them among available processors.
         */
        int sum = 0;

        Parallel.For(1, 101, i =>
        {
            sum += i;
        });

        Console.WriteLine("Sum of numbers from 1 to 100: " + sum);

        /* Parallel.ForEach: This construct is used to parallelize a foreach loop. 
         * It processes elements of a collection in parallel, spreading the work across multiple threads. 
         * It's especially useful when working with collections, such as arrays or lists.
         * 
         */
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        double sumSquares = 0.0;

        Parallel.ForEach(numbers, number =>
        {
            double square = Math.Pow(number, 2);
            sumSquares += square;
        });

        Console.WriteLine("Sum of squares: " + sumSquares);

        // Using TaskCreationOptions for Configuring Tasks
        Console.WriteLine("\nUsing TaskCreationOptions for Configuring Tasks\n");

        // Creating a task with Task.Factory
        _ = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Task started.");
        }, TaskCreationOptions.None);

        // Creating a long-running task
        _ = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Long-running task started.");
            // Simulate a CPU-bound operation
            for (int i = 0; i < 1000000; i++)
            {
                Math.Sqrt(i);
            }
        }, TaskCreationOptions.LongRunning);

        // Creating an attached child task
        Task parentTask = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Parent task started.");
            // Create an attached child task
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Child task started.");
            }, TaskCreationOptions.AttachedToParent);
        });

        // Wait for the parent task to complete
        parentTask.Wait();

        Console.WriteLine("All tasks completed.");
    }

    // In TPL, the basic unit of work is a Task.
    // A Task represents a single asynchronous operation that can run concurrently with other tasks.
    // You can create tasks explicitly using the Task class or use convenience methods like Task.Run().
    public static async Task<int> DoAsyncWork()
    {
        await Task.Delay(2000);
        return 88;
    }

    public static async Task<int> DoAsyncWorkException(CancellationToken token)
    {
        await Task.Delay(2000, token);
        throw new InvalidOperationException("Something went wrong!");
    }

    public static int Compute(int taskId)
    {
        Console.WriteLine($"Task {taskId} is computing...");
        Task.Delay(1000).Wait(); // Simulate a time-consuming operation
        return taskId * 10;
    }
}
