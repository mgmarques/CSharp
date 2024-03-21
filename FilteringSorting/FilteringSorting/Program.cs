using FilteringSorting.Models;
using FilteringSorting.Models.Enums;
using FilteringSorting.Services;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;

namespace FilteringSorting;

class Program
{
    // A LINQ Query Application Filtering and Sorting a List of Products
    public const int LineSize = 60;

    static void Main(string[] args)
    {
        Console.Write($"LINQ Query Application Filtering and Sorting Products");

        Console.WriteLine($"\n{new string('-', LineSize)}");
        string workingDirectory = Environment.CurrentDirectory;
        string project = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        Console.Write($"\nReadin file from path: {project}/Resources");
        string path = $"{project}/Resources/products.csv";

        List<Product> products = new List<Product>();

        using (StreamReader sr = File.OpenText(path))
        {
            while (!sr.EndOfStream)
            {
                string[] fields = sr.ReadLine().Split(',');
                long id = long.Parse(fields[0]);
                string name = fields[1];
                Categories categories;
                Categories category = (Categories) (
                    Enum.TryParse(value: fields[2], out categories) ? int.Parse(fields[2]) : 0);

                double price = double.Parse(fields[3], CultureInfo.InvariantCulture);
                products.Add(new Product(id, name, category, price));
            }
        }
        Console.WriteLine($"\nTotal records: {products.Count}");

        var avg = products.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
        Console.WriteLine($"Average price = {avg.ToString("F2", CultureInfo.InvariantCulture)}\n");

        Console.Write("\nPress any key to continue.");
        Console.ReadKey();

        // Filter the products by a specific category and sort by price in ascending order.
        int categoryNum = 0;
        Type categoryType = typeof(Categories);
        bool endApp = false;

        while (!endApp)
        {
            do
            {
                Console.Clear();
                EnumServices.ListEnums(categoryType, "category");
                try
                {
                    categoryNum = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    if (!Enum.IsDefined(categoryType, categoryNum))
                    {
                        Console.WriteLine("Wrong numember of category inputed! " +
                            "Press any key to try again!");
                        Console.ReadKey();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong choice! Select a number from the list! " +
                        "Press any key to try again!");
                    categoryNum = -1;
                    Console.ReadKey();
                }
            } while (!Enum.IsDefined(categoryType, categoryNum));

            Console.Clear();
            Console.WriteLine($"Products Report from {Enum.GetName(categoryType, categoryNum)}");
            Console.WriteLine($"{new string('-', LineSize)}");
            var query = from product
                        in products
                        where product.Category == (Categories)categoryNum
                        orderby product.Price
                        select new { Product = product.Name, Price = product.Price };

            foreach (var product in query)
            {
                Console.WriteLine($"Product: {product.Product}  - Price: {product.Price}");
            }

            avg = query.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine($"\nIts average price = {avg.ToString("F2", CultureInfo.InvariantCulture)}\n");
            // Wait for the user to respond before closing.
            Console.Write("Press 'n' to close the app, or press any other key to continue: ");
            if (Console.ReadKey().Key.ToString().ToLower() == "n") endApp = true;
        }
    }
}
