namespace Multi_threadedDataProcessingApplication;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    public const int LineSize = 51;

    static async Task Main(string[] args)
    {
        bool endApp = false;
        while (!endApp)
        {
            Console.Clear();
            Console.WriteLine("Multi-threaded Data Processing Application Example:");
            Console.WriteLine($"{new string('-', LineSize)}");
            // Ask the user to type the first number.
            Console.Write("Enter the size of the numbers list to proces, " +
                "and then press Enter: ");
            var sizeInput = Console.ReadLine();
            int sizeList = 0;
            while (!int.TryParse(sizeInput, out sizeList))
                {
                Console.Clear();
                Console.WriteLine("Multi-threaded Data Processing Application Example:");
                Console.WriteLine($"{new string('-', LineSize)}");
                Console.Write("This is not valid input. " +
                        "Please enter a valid number for the list size: ");
                    sizeInput = Console.ReadLine();
                }

            // Generate a list of integers
            List<int> data = GenerateData(sizeList);
            // Set the chunk size for dividing the data
            int chunkSize = 10;

            // Start the System.Diagnostics.Stopwatch to evaluate the performance
            Stopwatch stopwatch = Stopwatch.StartNew(); 

            List<Task<int>> processingTasks = new List<Task<int>>();

            for (int i = 0; i < data.Count; i += chunkSize)
            {
                List<int> chunk = data.Skip(i).Take(chunkSize).ToList();
                // Process each chunk concurrently
                processingTasks.Add(ProcessChunkAsync(chunk)); 
            }

            // Use exception handling to manage any errors that occur during task execution.
            try
            {
                // Utilize the Task.WhenAll method to wait for all processing tasks to complete.
                await Task.WhenAll(processingTasks);
                // Aggregate the results
                int totalSum = processingTasks.Sum(t => t.Result);
                // Stop the stopwatche.
                stopwatch.Stop(); 

                Console.WriteLine($"Total sum of all chunks: {totalSum}");
                // Use the stopwatch to measure and display the processing time.
                Console.WriteLine($"Total processing time: " +
                    $"{stopwatch.ElapsedMilliseconds} ms");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"\nA list with {sizeList} elements is too big to process!");
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    Console.WriteLine(
                        "{0}:\n   {1}", e.GetType().Name, e.Message);
                }
            }

                // Wait for the user to respond before closing.
                Console.Write("\nPress 'n' to close the app, or press any other key to continue: ");
                if (Console.ReadKey().Key.ToString().ToLower() == "n") endApp = true;
            }
    }

    static List<int> GenerateData(int count)
    {
        List<int> data = new List<int>();
        for (int i = 1; i <= count; i++)
        {
            data.Add(i);
        }
        return data;
    }

    static async Task<int> ProcessChunkAsync(List<int> chunk)
    {
        /*Implement a simple processing function that, for example, calculates 
         * the sum of integers in each chunk and uses await Task.Delay to simulate work.
         */
        // Simulate work with a delay
        await Task.Delay(100);
        // Calculate the sum of integers in the chunk
        return chunk.Sum(); 
    }
}
