using System;
using System.Threading.Tasks;

public class CustomException : Exception
{
    public CustomException()
    {
    }

    public CustomException(string message)
        : base(message)
    {
    }

    public CustomException(string message, Exception inner)
        : base(message, inner)
    {
    }
}


public static partial class Program
{
    public static void ExceptionPropagationTwo()
    {
        _ = Task.Run(
            () => throw new CustomException("task1 faulted."))
            .ContinueWith(_ =>
            {
                // A continuation that runs only if the antecedent task faults
                if (_.Exception?.InnerException is { } inner)
                {
                    Console.WriteLine("{0}: {1}",
                        inner.GetType().Name,
                        inner.Message);
                    Console.Write("\nPress any key to close the app.");
                }
            },
            TaskContinuationOptions.OnlyOnFaulted);

        Thread.Sleep(500);
    }

    static void ExecuteTasks()
    {
        // Assume this is a user-entered String.
        string path = @"C:\";
        List<Task> tasks = new();

        tasks.Add(Task.Run(() =>
        {
            // This should throw an UnauthorizedAccessException.
            return Directory.GetFiles(
                path, "*.txt",
                SearchOption.AllDirectories);
        }));

        tasks.Add(Task.Run(() =>
        {
            if (path == @"C:\")
            {
                throw new ArgumentException(
                    "The system root is not a valid path.");
            }
            return new string[] { ".txt", ".dll", ".exe", ".bin", ".dat" };
        }));

        tasks.Add(Task.Run(() =>
        {
            throw new NotImplementedException(
                "This operation has not been implemented.");
        }));

        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException ae)
        {
            throw ae.Flatten();
        }
    }

    public static void FlattenTwo()
    {
        var task = Task.Factory.StartNew(() =>
        {
            var child = Task.Factory.StartNew(() =>
            {
                var grandChild = Task.Factory.StartNew(() =>
                {
                    // This exception is nested inside three AggregateExceptions.
                     throw new CustomException("Attached child2 faulted.");
                }, TaskCreationOptions.AttachedToParent);

                // This exception is nested inside two AggregateExceptions.
                throw new CustomException("Attached child1 faulted.");
            }, TaskCreationOptions.AttachedToParent);
        });

        try
        {
            task.Wait();
        }
        catch (AggregateException ae)
        {
            foreach (var ex in ae.Flatten().InnerExceptions)
            {
                if (ex is CustomException)
                {
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    throw;
                }
            }
        }
    }

    public static void HandleFour()
    {
        var task4 = Task.Run(
            () => throw new CustomException("This exception is expected one!"));

        // This example code includes a while loop that polls the task's Task.IsCompleted
        // property to determine when the task has completed.
        // This should never be done in production code as it is very inefficient.
        while (!task4.IsCompleted) { }
        Console.WriteLine("It enter here!");

        if (task4.Status == TaskStatus.Faulted)
        {
            foreach (var ex in task4.Exception?.InnerExceptions ?? new(Array.Empty<Exception>()))
            {
                // Handle the custom exception.
                if (ex is CustomException)
                {
                    Console.WriteLine(ex.Message);
                }
                // Rethrow any other exception.
                else
                {
                    throw ex;
                }
            }
        }
    }

    public static void Main()
    {
        ExceptionPropagationTwo();
        Console.ReadKey();

        try
        {
            /* Use the AggregateException.Flatten method to rethrow the inner exceptions
             * from multiple AggregateException instances thrown by multiple tasks in a 
             * single AggregateException instanc
             */
            Console.WriteLine("\nRethrow the inner exceptions:");
            Console.WriteLine("-----------------------------\n");
            ExecuteTasks();
        }
        catch (AggregateException ae)
        {
            foreach (var e in ae.InnerExceptions)
            {
                Console.WriteLine(
                    "{0}:\n   {1}", e.GetType().Name, e.Message);
            }
        }
        Console.Write("\nPress any key to close the app.");
        Console.ReadKey();

        Console.WriteLine("\n\nFlatten InnerExceptions:");
        Console.WriteLine("------------------------\n");
        FlattenTwo();
        Console.Write("\nPress any key to close the app.\n");
        Console.ReadKey();

        var task = Task.Run(
            () => throw new CustomException("This exception is expected!"));

        try
        {
            Console.WriteLine("\n\nHandle expected exception!");
            Console.WriteLine("--------------------------\n");
            Console.WriteLine("Task 2");
            task.Wait();
            Console.WriteLine("But not enter here!");

        }
        catch (AggregateException ae)
        {
            foreach (var ex in ae.InnerExceptions)
            {
                // Handle the custom exception.
                if (ex is CustomException)
                {
                    Console.WriteLine(ex.Message);
                }
                // Rethrow any other exception.
                else
                {
                    throw ex;
                }
            }
        }
        Console.Write("\nPress any key to close the app.");
        Console.ReadKey();

        Console.WriteLine("\n\nRetrieve the AggregateException exception from the task's Exception property:");
        Console.WriteLine("-----------------------------------------------------------------------------\n");
        HandleFour();
        Console.Write("\nPress any key to close the app.");
        Console.ReadKey();

    }
}
