using System;
using System.Threading.Tasks.Dataflow;

public class DataflowExample
{
    public static async Task Main(string[] args)
    {
        var multiplyBlock = new TransformBlock<int, int>(x => x * 2);
        var addBlock = new TransformBlock<int, int>(x => x + 3);
        var printBlock = new ActionBlock<int>(x => Console.WriteLine($"Result: {x}"));

        multiplyBlock.LinkTo(addBlock);
        addBlock.LinkTo(printBlock);

        multiplyBlock.Post(5); // Start the dataflow

        await printBlock.Completion;
    }
}
