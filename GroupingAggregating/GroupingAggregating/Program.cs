using GroupingAggregation.Models;
using GroupingAggregation.Models.Enums;
using GroupingAggregation.Services;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Transactions;

namespace GroupingAggregation;

class Program
{
    // A LINQ Query Application Filtering and Sorting a List of Products
    public const int LineSize = 60;

    static void Main(string[] args)
    {
        Console.Write($"LINQ Transactions Grouping and Aggregations:");

        Console.WriteLine($"\n{new string('-', LineSize)}");
        string workingDirectory = Environment.CurrentDirectory;
        string project = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        Console.Write($"\nReading a file from path: {project}/Resources");
        string path = $"{project}/Resources/transactions.csv";

        List<Models.Transaction> transactions = new List<Models.Transaction>();

        using (StreamReader sr = File.OpenText(path))
        {
            while (!sr.EndOfStream)
            {
                string[] fields = sr.ReadLine().Split(',');
                string transactionId = fields[0];
                string producId = fields[1];
                int quantity = int.Parse(fields[2]);
                double price = double.Parse(fields[3], CultureInfo.InvariantCulture);
                transactions.Add(new Models.Transaction(transactionId, producId, quantity, price));
            }
        }
        Console.WriteLine($"\nTotal records: {transactions.Count}");
        Console.Write("\nPress any key to continue.");
        Console.ReadKey();

        // LINQ to group transactions by ProductId and then aggregate the data to calculate the total sales
        Console.Clear();
        Console.WriteLine($"Sales Report:");
        Console.WriteLine($"{new string('-', LineSize)}");
        var query = from t in (
                        from transaction
                        in transactions
                        group transaction by transaction.TransactionId into g
                        select new { TransactionId = g.Key, Sales = g.Sum(p => p.Price * p.Quantity) }
                     )
                    orderby t.Sales descending
                    select t;

        foreach (var sale in query)
        {
            Console.WriteLine($"TransactionId: {sale.TransactionId}  -" +
                $" Total Sales: {sale.Sales.ToString("F2", CultureInfo.InvariantCulture)}");
        }

        double total = query.Select(p => p.Sales).DefaultIfEmpty(0.0).Sum();
        Console.WriteLine($"\nTotal sales value = {total.ToString("F2", CultureInfo.InvariantCulture)}\n");
        var avg = transactions.Select(p => p.Price * p.Quantity).DefaultIfEmpty(0.0).Average();
        Console.WriteLine($"Average sales value by invoce item = {avg.ToString("F2", CultureInfo.InvariantCulture)}\n");

        // Wait for the user press any key before closing.
        Console.Write("Press any key to close the app.");
        Console.ReadKey();
    }
}
