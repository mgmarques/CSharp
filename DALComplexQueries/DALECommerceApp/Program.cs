using System;
using System.Globalization;
using System.Reflection.Metadata;
using System.Linq;
using System.Threading.Tasks;
using DALECommerceApp.Models;
using DALECommerceApp.Models.Data;
using DALECommerceApp.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;
using System.Diagnostics.Metrics;

namespace DALECommerceApp;

class Program
{
    public const int LineSize = 82;

    //Create DBContext object
    static void OrderReport(AppDbContext db, string message)

    {
        // LINQ to query all the deails from the order of customer 1
        var query = from _orderItem in db.OrderItems
                    join _product in db.Products on _orderItem.ProductId equals _product.ProductId
                    join _order in db.Orders on _orderItem.OrderId equals _order.OrderId
                    join _customer in db.Customers on _order.CustomerId equals _customer.CustomerId
                    where _customer.CustomerId == 1
                    select new
                    {
                        Name = _customer.ClientName,
                        Kind = _customer.CustomerKind,
                        OrderId = _order.OrderId,
                        Created = _order.Created,
                        OrderStatus = _order.Status,
                        DateToDelivery = _order.DateToDelivery,
                        DeliveryDate = _order.DeliveryDate,
                        Prduct = $"{_product.Category}: {_product.Name}",
                        Quantity = _orderItem.Quantity,
                        TotalItemValue = _orderItem.InvoiceItemAmount
                    };

        // Display the order current status and details from the database
        Console.Clear();
        Console.WriteLine(message);
        Console.WriteLine($"{new string('-', LineSize)}");
        bool firstLine = true;
        string pattern = "MM-dd-yyyy";
        foreach (var item in query)
        {
            if (firstLine)
            {
                Console.WriteLine($"Client: {item.Kind}: {item.Name}");
                Console.WriteLine($"{new string('-', LineSize)}");
                Console.WriteLine($"    Order: {item.OrderId}: {item.OrderStatus}  " +
                    $" - Created: {item.Created.ToString(pattern)} " +
                    $"Date to Delivery: {item.DateToDelivery.ToString(pattern)}" +
                    $"   Delivery Date: {item.DeliveryDate}" 
                    );
                firstLine = false;
            }
            Console.WriteLine($"       * Value: {item.TotalItemValue.ToString("C2", CultureInfo.CurrentCulture)}" +
                $" | Quantity: {item.Quantity} | {item.Prduct}");
        }

        var queryTransactions =
            from t in (
                        from _orderItem in db.OrderItems
                        join _order in db.Orders on _orderItem.OrderId equals _order.OrderId
                        join _customer in db.Customers on _order.CustomerId equals _customer.CustomerId
                        where _customer.CustomerId == 1
                        group _orderItem by _orderItem.OrderId into g
                        select new
                        {
                            Order = g.Key,
                            TotalItemValue = g.Sum(p => p.Quantity * p.Price),
                            AverageItemValue = g.Average(p => p.Quantity * p.Price)
                        })
            orderby t.TotalItemValue descending
            select t;

        Console.WriteLine($"\nSales Summary:");
        Console.WriteLine($"{new string('-', LineSize)}");

        foreach (var item in queryTransactions)
        {
            Console.WriteLine($"Invoice Total Value: {item.TotalItemValue.ToString("C2", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"Invoice Average Value by Item: {item.AverageItemValue.ToString("C2", CultureInfo.CurrentCulture)}");
        }

        // Wait for the user press any key before closing.
        Console.Write("\nPress any key to continue.");
        Console.ReadKey();
    }

    static void Main(string[] args)
    {
        using var db = new AppDbContext();

        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");

        // Clean the database before start
        try
        {
            db.Products.ExecuteDelete();
            db.Customers.ExecuteDelete();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}"); ;
        }

        string workingDirectory = Environment.CurrentDirectory;
        string project = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        Console.Write($"\nReading files from path: {project}/Resources");
        string path = $"{project}/Resources/customers.csv";
        int records = 0;

        try
        {
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    long customerId = long.Parse(fields[0]);
                    string clientName = fields[1];
                    CustomerKinds customerKind = Enum.Parse<CustomerKinds>(fields[2]);
                    string email = fields[3];
                    string password = fields[4];
                    DateTime created = DateTime.Parse(fields[5], CultureInfo.InvariantCulture);
                    var customer = new Customer
                    {
                        CustomerId = customerId,
                        ClientName = clientName,
                        CustomerKind = customerKind,
                        Email = email,
                        Password = password,
                        Created = created
                    };
                    _ = db.Customers.Add(customer);
                    records += 1;
                }
                // Save the Customers in the Database
                db.SaveChanges();
            }
            Console.WriteLine($"\nTotal Customers records: {records}");

            path = $"{project}/Resources/products.csv";
            records = 0;
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    long productId = long.Parse(fields[0]);
                    string name = fields[1];
                    double unitPrice = double.Parse(fields[2]);
                    Categories category = Enum.Parse<Categories>(fields[3]);
                    var product = new Product
                    {
                        ProductId = productId,
                        Name = name,
                        UnitPrice = unitPrice,
                        Category = category
                    };
                    _ = db.Products.Add(product);
                    records += 1;
                }
                // Save the Products in the Database
                db.SaveChanges();
            }
            Console.WriteLine($"\nTotal Products records: {records}");

            path = $"{project}/Resources/orders.csv";
            records = 0;
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    long orderId = long.Parse(fields[0]);
                    long customerId = long.Parse(fields[1]);
                    OrderStatus status = Enum.Parse<OrderStatus>(fields[2]);
                    SaleStatus saleStatus = Enum.Parse<SaleStatus>(fields[3]);
                    DateTime created = DateTime.Parse(fields[4], CultureInfo.InvariantCulture);
                    DateTime dateToDelivery = DateTime.Parse(fields[5], CultureInfo.InvariantCulture);
                    // DeliveryDate
                    string delivered = fields[6];

                    var order = new Order
                    {
                        OrderId = orderId,
                        CustomerId = customerId,
                        Status = status,
                        SaleStatus = saleStatus,
                        Created = created,
                        DateToDelivery = dateToDelivery
                    };
                    if (delivered != "")
                    {
                        order.DeliveryDate = DateTime.Parse(fields[6], CultureInfo.InvariantCulture);
                    }

                    _ = db.Orders.Add(order);
                    records += 1;
                }
                // Save the Order in the Database
                db.SaveChanges();
            }
            Console.WriteLine($"\nTotal Orders records: {records}");

            path = $"{project}/Resources/order_items.csv";
            records = 0;
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    long orderId = long.Parse(fields[0]);
                    long productId = long.Parse(fields[1]);
                    int quantity = int.Parse(fields[2]);
                    double price = double.Parse(fields[3]);

                    var orderItems = new OrderItem
                    {
                        OrderId = orderId,
                        ProductId = productId,
                        Quantity = quantity,
                        Price = price
                    };

                    _ = db.OrderItems.Add(orderItems);
                    records += 1;
                }
                // Save the Order Items in the Database
                db.SaveChanges();
            }
            Console.WriteLine($"\nTotal Order Items records: {records}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError at saving the data read from {path}! {ex.Message}");
        }
        Console.Write("\nPress any key to continue.");
        Console.ReadKey();

        // Total Sales per Product:
        Console.Clear();
        Console.WriteLine("Total Sales per Product:\n");

        var querySalesProducts =
            from t in (
                        from _orderItem in db.OrderItems 
                        join _order in db.Orders on _orderItem.OrderId equals _order.OrderId
                        join _product in db.Products on _orderItem.ProductId equals _product.ProductId
                        where _order.SaleStatus == SaleStatus.Billed
                        group _orderItem by _product.Name into g
                        select new
                        {
                            Product = g.Key,
                            TotalSales = g.Sum(p => p.Quantity * p.Price),
                            Amount = g.Sum(p => p.Quantity)
                        })
            orderby t.TotalSales descending
            select t;

        Console.WriteLine("{0,-56}{1,-1}{2,-1}", "Product:", "| Amount:", "| Value:");
        Console.WriteLine($"{new string('-', 56)}+{new string('-', 8)}+{new string('-', 15)}");
        foreach (var product in querySalesProducts)
        {
            Console.WriteLine($"{String.Format("{0,-55}", product.Product)} | " 
                + $"{String.Format("{0,6}", product.Amount)} | " 
                + $"{String.Format("{0,13}", product.TotalSales.ToString("c2", CultureInfo.CurrentCulture))}");
        }

        // Wait for the user press any key before closing.
        Console.Write("\nPress any key to continue.");
        Console.ReadKey();

        // Products Never Ordered:
        Console.Clear();
        Console.WriteLine("Products Never Ordered:\n");

        var queryProductsNeverOrdered =
            from _product in db.Products
            join _orderItens in db.OrderItems on _product.ProductId equals _orderItens.ProductId into _Table
            from _t in _Table.DefaultIfEmpty()
            where _t == null ? true : false
            select new
            {
                ID =_product.ProductId,
                Name = _product.Name,
                Category = _product.Category
            };

        Console.WriteLine("{0,-7}{1,-51}{2,-10}", "ID:", "| Product Name:", "| Category:");
        Console.WriteLine($"{new string('-', 7)}+{new string('-', 50)}+{new string('-', 15)}");
        foreach (var product in queryProductsNeverOrdered)
        {
            Console.WriteLine($"{String.Format("{0,-6}", product.ID)} | "
                + $"{String.Format("{0,-48}", product.Name)} | "
                + $"{product.Category}");
        }

        // Wait for the user press any key before closing.
        Console.Write("\nPress any key to continue.");
        Console.ReadKey();

        // High-Value Customers:
        Console.Clear();
        Console.WriteLine("High-Value Customers (greater than $ 10000):\n");

        var queryHighValueCustomers =
            from t in (
                        from _orderItem in db.OrderItems
                        join _order in db.Orders on _orderItem.OrderId equals _order.OrderId
                        join _customer in db.Customers on _order.CustomerId equals _customer.CustomerId
                        where _order.SaleStatus == SaleStatus.Billed
                        group _orderItem by _customer.ClientName into g
                        select new
                        {
                            Customer = g.Key,
                            TotalSales = g.Sum(p => p.Quantity * p.Price),
                            Amount = g.Sum(p => p.Quantity)
                        })
            where t.TotalSales > 10000
            orderby t.TotalSales descending

            select t;

        Console.WriteLine("{0,-51 }{1,2}{2,1}", "Customer:", "| Amount: ", "| Value:");
        Console.WriteLine($"{new string('-', 51)}+{new string('-', 9)}+{new string('-', 15)}");
        foreach (var customer in queryHighValueCustomers)
        {
            Console.WriteLine($"{String.Format("{0,-50}", customer.Customer)} | "
                + $"{String.Format("{0,7}", customer.Amount)} | "
                + $"{String.Format("{0,13}",customer.TotalSales.ToString("C2", CultureInfo.CurrentCulture))}");
        }

        // Wait for the user press any key before closing.
        Console.WriteLine("\nPress any key to close the app.");
        Console.ReadKey();
        try
        {
            db.Products.ExecuteDelete();
            db.Customers.ExecuteDelete();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}"); ;
        }
    }
}
