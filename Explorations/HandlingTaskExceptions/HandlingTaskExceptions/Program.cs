using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HandlingTaskExceptions;

class Program
{
    //This is not real code only an example
    static void Main(string[] args)
    {
        Console.WriteLine("Starting...\n");
        try
        {
            DoTasks();
        }
        catch (AggregateException ex)
        {
            foreach (Exception inner in ex.InnerExceptions)
            {
                Console.WriteLine("Exception type {0} from {1}",
                    inner.GetType(), inner.Source);
            }
        }
        Console.WriteLine("\nEnding.");
    }

    static void DoTasks()
    {
        // Run Stuff in BackGround
        Task.Factory.StartNew(() =>
        {
            //Do this in the background
            SomeIntensiveJob();
        }).ContinueWith(task =>
        {
            Console.WriteLine($"\nCheck: {task.IsFaulted}\n");

            //Check to see if there was a exception on the task handle it.
            if (task.IsFaulted)
            {
                string exMessage = "unknown error";
                string exStack = "unknow Exception is null";
                if (task.Exception != null)
                {
                    //Log all the Exceptions (maybe lots)
                    foreach (var ex in task.Exception.InnerExceptions)
                    {
                        exMessage = ex.Message;
                        exStack = ex.StackTrace;
                        string errlog = $"Stuff , Exception : {exMessage} ,  Stack :  {exStack} ";
                        Console.WriteLine("treat...\n");
                        Console.WriteLine(errlog);
                    }
                }

                Console.WriteLine($"Error on stuff {exMessage}, with stack {exStack}");
                return; // exit on error 
            } //////////////////////////  end exception handling ///////////////////////////////////
              // do this on the UI thread once the task has finished..
              // The task has completed without any issues so do your stuff
            runPostTaksStuff();
        }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

    }

    private static void runPostTaksStuff()
    {
        throw new NotImplementedException();
    }

    private static void SomeIntensiveJob()
    {
        throw new NotImplementedException();
    }
}
