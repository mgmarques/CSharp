using System;
using System.Globalization;
using System.Reflection.Metadata;
using System.Linq;
using System.Threading.Tasks;
using DALECommerceApp.Models;
using DALECommerceApp.Models.Data;
using DALECommerceApp.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DALECommerceApp;

class Program
{
    public const int LineSize = 80;

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
            Console.WriteLine($"       * Value: {item.TotalItemValue.ToString("F2", CultureInfo.InvariantCulture)}" +
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
            Console.WriteLine($"Invoice Total Value: {item.TotalItemValue.ToString("F2", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Invoice Average Value by Item: {item.AverageItemValue.ToString("F2", CultureInfo.InvariantCulture)}");
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

        string clientName = "", email = "", password = "";
        CustomerKinds customerKind = CustomerKinds.Person;
        bool nok = true;
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
        do
        {
            try
            {
                // Create a new Customer
                Console.Clear();
                Console.WriteLine("Enter customer data: ");
                Console.WriteLine($"{new string('-', LineSize)}");

                Console.Write("Name: ");
                clientName = Console.ReadLine();

                Console.Write("Kind: ");
                customerKind = Enum.Parse<CustomerKinds>(Console.ReadLine());

                Console.Write("Email: ");
                email = Console.ReadLine();

                Console.Write("Password: ");
                password = Console.ReadLine();

                nok = false;
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Please, review your entery and try again! Details: {ex.Message} ");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
        } while (nok);



        var customer = new Customer
        {
            CustomerId = 1,
            ClientName = clientName,
            CustomerKind = customerKind,
            Email = email,
            Password = password,
            Created = DateTime.Today
        };
        db.Customers.Add(customer);

        try
        {
            // Save the new client in the Database
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at saving the new client! {ex.Message}");
        }

        // Create a few new Products
        var product = new Product
        {
            ProductId = 1,
            Name = "X-BOX Series X",
            UnitPrice = 499.99,
            Category = Categories.GameConsole
        };
        db.Products.Add(product);

        product = new Product
        {
            ProductId = 2,
            Name = "X-BOX Series Wireless Controler",
            UnitPrice = 159.99,
            Category = Categories.GameControler
        };
        db.Products.Add(product);

        product = new Product
        {
            ProductId = 3,
            Name = "X-BOX Series Controler",
            UnitPrice = 59.99,
            Category = Categories.GameControler
        };
        db.Products.Add(product);

        try
        {
            // Save the Products in the Database
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at save the products! {ex.Message}");
        }

        // Create a new Order
        var order = new Order
        {
            OrderId = 1,
            CustomerId = 1,
            Status = OrderStatus.PaymentConfirmed,
            SaleStatus = SaleStatus.Billed,
            Created = DateTime.Today,
            DateToDelivery = DateTime.Today.AddDays(10)
        };
        db.Orders.Add(order);

        // Create the Order Itens
        var orderItems = new OrderItem
        {
            OrderId = 1,
            ProductId = 1,
            Quantity = 1,
            Price = 599.99
        };
        db.OrderItems.Add(orderItems);

        orderItems = new OrderItem
        {
            OrderId = 1,
            ProductId = 2,
            Quantity = 2,
            Price = 149.99
        };
        db.OrderItems.Add(orderItems);

        orderItems = new OrderItem
        {
            OrderId = 1,
            ProductId = 3,
            Quantity = 2,
            Price = 57.75
        };
        db.OrderItems.Add(orderItems);

        // Save all chages in the Database
        db.SaveChanges();

        OrderReport(db, "See the order entry status:");

        // Read
        try
        {
            Console.Clear();
            Console.WriteLine("Querying the OrderId 1");
            Console.WriteLine($"{new string('-', LineSize)}");
            // Fetch the Order from database whose OrderId = 1
            var orderQuery = db.Orders.Find(1L);
            if (orderQuery != null)
            {
                // At this point Entity State will be Unchanged
                Console.WriteLine($"Before Updating Entity State: {db.Entry(orderQuery).State}");
                // Update the order data
                orderQuery.DeliveryDate = DateTime.UtcNow.AddDays(2);
                orderQuery.Status = OrderStatus.Delivered;
                db.Orders.Update(orderQuery);
                // At this point Entity State will be Modified
                Console.WriteLine($"After Updating Entity State: {db.Entry(orderQuery).State}");

                // Call SaveChanges method to update Order data into database
                db.SaveChanges();
                // Now the Entity State will change from Modified State to Unchanged State
                Console.WriteLine($"After SaveChanges Entity State: {db.Entry(orderQuery).State}");
            }
            else
            {
                Console.WriteLine("Invalid Student ID : 1");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
        }
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();

        OrderReport(db, "Updating the Order Delivery date and status");

        // Delete Operations
        try
        {
            // Find the Order to be deleted by OrderId
            var orderQuery = db.Orders.Find(1L);

            if (orderQuery != null)
            {
                Console.Clear();
                Console.WriteLine("Delete Order with OrderId 1:");
                Console.WriteLine($"{new string('-', LineSize)}");
                // At this point the Entity State will be Unchanged
                Console.WriteLine($"Entity State Before Removing: {db.Entry(orderQuery).State}");

                // The following statement mark the Entity State as Deleted
                db.Orders.Remove(orderQuery);

                // SaveChanges method will delete the Entity from the database
                db.SaveChanges();

                // Once the SaveChanges Method executed successfully, 
                // the Entity State will be in Detached state
                Console.WriteLine($"Entity State After Removing: {db.Entry(orderQuery).State}");
            }
            else
            {
                Console.WriteLine("Invalid Order ID: 1");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}"); ;
        }

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
