using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class ProducerConsumerExample
{
    public static async Task Main(string[] args)
    {
        var buffer = new BlockingCollection<int>(boundedCapacity: 10);

        var producer = Task.Run(() =>
        {
            for (int i = 1; i <= 20; i++)
            {
                buffer.Add(i);
                Console.WriteLine($"Produced: {i}");
            }
            buffer.CompleteAdding();
        });

        var consumer = Task.Run(() =>
        {
            foreach (var item in buffer.GetConsumingEnumerable())
            {
                Console.WriteLine($"Consumed: {item}");
            }
        });

        await Task.WhenAll(producer, consumer);
    }
}
