using System.Linq;
using ExtensionMethods;
using Domain;
using static System.Net.Mime.MediaTypeNames;

namespace extensions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Linq Simple Samples:");
        Console.WriteLine(new string('_', 60));
        Console.WriteLine("Order By:");
        int[] ints = { 10, 45, 15, 39, 21, 26 };
        var result = ints.OrderBy(g => g);
        foreach (int i in result)
        {
            Console.Write(i + " ");
        }

        // Specify the data source.
        int[] scores = { 97, 92, 81, 60 };

        // Define the query expression.
        IEnumerable<int> scoreQuery =
            from score in scores
            where score > 80
            select score;

        Console.WriteLine("\nLINQ Score grater than 80:");
        // Execute the query.
        foreach (var i in scoreQuery)
        {
            Console.Write(i + " ");
        }

        IList<Author> authors = new List<Author>() {
                        new Author() { Id = 1, FirstName = "John",
                                      LastName = "Willey"},
                        new Author() { Id = 2, FirstName = "Steve",
                                      LastName = "Smith"},
                        new Author() { Id = 3, FirstName = "Bill",
                                      LastName = "Ruffner"},
                        new Author() { Id = 4, FirstName = "Joydip",
                                      LastName = "Kanjilal"}
                };
        var query = from a in authors
                     select new
                     {
                         Id = a.Id,
                         Name = a.FirstName + "\t" + a.LastName
                     };
        Console.WriteLine("\nAnonymous types:");
        foreach (var data in query)
            Console.WriteLine(data.Name);

        System.Console.WriteLine("\nEstensions:");
        Console.WriteLine(new string('_', 60));
        string s = "Hello Extension Methods";
        int w = s.WordCount();
        Console.WriteLine($"{s} has {w} words.");

        Console.WriteLine("\nLayer-Specific Functionality:");
        DomainEntity d = new DomainEntity(1, "Marcia", "Leão");

        Console.WriteLine(d.FullName());

        Console.WriteLine("\nExtending Predefined Types:");
        int x = 1;

        // Takes x by value leading to the extension method
        // Increment modifying its own copy, leaving x unchanged
        x.Increment();
        Console.WriteLine($"x did not change {x}");

        // Takes x by reference leading to the extension method
        // RefIncrement changing the value of x directly
        x.RefIncrement();
        Console.WriteLine($"Since x is a ref for the extecion, it is now {x}");

        Console.WriteLine("\nThis next example demonstrates ref extension methods for user - defined struct types :");
        Account account = new()
        {
            id = 1,
            balance = 100f
        };

        Console.WriteLine($"I have ${account.balance}"); 

        account.Deposit(50f);
        Console.WriteLine($"I have ${account.balance}");

        Console.WriteLine("\nHow to create a new method for an enumeration:");
        Grades g1 = Grades.D;
        Grades g2 = Grades.F;
        Console.WriteLine("First {0} a passing grade.", g1.Passing() ? "is" : "is not");
        Console.WriteLine("Second {0} a passing grade.", g2.Passing() ? "is" : "is not");

        MyExtensions.minPassing = Grades.C;
        Console.WriteLine("\r\nRaising the bar!\r\n");
        Console.WriteLine("First {0} a passing grade.", g1.Passing() ? "is" : "is not");
        Console.WriteLine("Second {0} a passing grade.", g2.Passing() ? "is" : "is not");

        Console.ReadKey();
    }
}

