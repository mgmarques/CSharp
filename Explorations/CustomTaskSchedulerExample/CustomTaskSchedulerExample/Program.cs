using System;
using System.Threading.Tasks;

public class CustomTaskSchedulerExample
{
    public static void Main(string[] args)
    {
        /* In this example, the ThreadAffinityTaskScheduler custom scheduler 
         * ensures that tasks are executed on the same thread where they were queued, 
         * demonstrating thread affinity. 
         */
        // Create a custom task scheduler with thread affinity
        var scheduler = new ThreadAffinityTaskScheduler();

        // Queue tasks to run on the custom scheduler
        Task.Factory.StartNew(() => PrintThreadId("Task 1"),
            CancellationToken.None, TaskCreationOptions.None, scheduler);
        Task.Factory.StartNew(() => PrintThreadId("Task 2"),
            CancellationToken.None, TaskCreationOptions.None, scheduler);

        Console.ReadKey();
    }

    public static void PrintThreadId(string taskName)
    {
        Console.WriteLine($"{taskName} is running on Thread ID " +
            $"{Environment.CurrentManagedThreadId}");
    }
}

public class ThreadAffinityTaskScheduler : TaskScheduler
{
    protected override void QueueTask(Task task)
    {
        // For demonstration, execute the task on the current thread (thread affinity)
        TryExecuteTask(task);
    }

    protected override bool TryExecuteTaskInline(Task task,
        bool taskWasPreviouslyQueued)
    {
        return false; // Don't execute inline
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return Enumerable.Empty<Task>();
    }
}
