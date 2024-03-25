using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class MapReduceExample
{
    public static async Task Main(string[] args)
    {
        List<int> data = Enumerable.Range(1, 1000).ToList();

        // Map: Square each number
        var mappedData = await Task.WhenAll(data.Select(async x => await SquareAsync(x)));

        // Reduce: Sum all squared values
        int sum = mappedData.Sum();

        Console.WriteLine("MapReduce Result: " + sum);
    }

    public static async Task<int> SquareAsync(int number)
    {
        await Task.Delay(10); // Simulate async work
        return number * number;
    }
}
