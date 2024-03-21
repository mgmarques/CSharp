using System.Drawing;
using System.Globalization;
using BasicInventorySystem.Entities;
using BasicInventorySystem.Entities.Enums;

namespace Course
{
    class Program
    {
        public const int LineSize = 50;

        static void ListEnums(Type enumType)
        {
            foreach (int i in Enum.GetValues((enumType)))
            {
                Console.WriteLine($" {i} - {Enum.GetName(enumType, i)}");
            };
        }

        static void Main(string[] args)
        {
            List<Item> list = new List<Item>();
            int itens = 0;
            bool nok = true;
            String name = "No name";
            String description = "No description";
            float weight = 0;
            double price =0;

            Console.Write("Enter the number of itens: ");
            do
            {
                try
                {
                    itens = int.Parse(Console.ReadLine());
                    nok = false;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.Write($"Please, enter a valid integer number! ");
                }    
            } while (nok);

            char type;
            char[] allowedDepartments = new char[] { 'b', 'e', 'f' };

            for (int i = 1; i <= itens; i++)
            {
                do
                {
                    Console.Clear();
                    Console.Write("Is this a (B)ook, (E)lectronic or (F)urtniture? ");
                    type = char.ToLower(Console.ReadKey().KeyChar);
                    if (!allowedDepartments.Contains(type))
                    {
                        Console.WriteLine($"Wrong Department {type}, " +
                            $"please make a choise from b, e or f");
                    }
                }
                while (!allowedDepartments.Contains(type));

                do
                {
                    nok = false;
                    Console.WriteLine($"\n{new string('-', LineSize)}");
                    Console.WriteLine("Item #" + i + " data:");

                    try
                    {
                        Console.Write("Name: ");
                        name = Console.ReadLine();

                        Console.Write("Description: ");
                        description = Console.ReadLine();

                        Console.Write("Weight: ");
                        weight = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                        Console.Write("Price: ");
                        price = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Wrong imput! {e}. Try again!");
                        nok = true;
                    }
                } while (nok);

                if (type == 'b')
                {
                    do
                    {
                        nok = false;
                        try
                        {
                            Console.WriteLine("Book details:");
                            Console.Write("Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Publisher: ");
                            string publisher = Console.ReadLine();
                            Console.Write("Publication date (MM/DD/YYYY): ");
                            DateTime publicationDate = DateTime.Parse(Console.ReadLine());
                            ListEnums(typeof(Genres));
                            Console.Write("Genre: ");
                            Genres genre = (Genres)int.Parse(Console.ReadLine());
                            list.Add(
                                new Books(name, description, weight, price,
                                genre, author, publisher, publicationDate)
                                );
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Wrong imput! {e}. Try again!");
                            nok = true;
                        }
                    } while (nok);
                }
                else if (type == 'e')
                {
                    do
                    {
                        nok = false;
                        try
                        {
                            ;
                            Console.Write("Model: ");
                            string model = Console.ReadLine();
                            Console.Write("Manufacture date (MM/DD/YYYY): ");
                            DateTime manufactureDate = DateTime.Parse(Console.ReadLine());
                            ListEnums(typeof(Electronic));
                            Console.Write("Electronic: ");
                            Electronic kind = (Electronic)int.Parse(Console.ReadLine());
                            list.Add(new Electronics(name, description, weight,
                                price, kind, model, manufactureDate));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Wrong imput! {e}. Try again!");
                            nok = true;
                        }
                    } while (nok);
                }
                else if (type == 'f')
                {
                    do
                    {
                        nok = false;
                        try
                        {
                            Console.Write("Manufacture date (MM/DD/YYYY): ");
                            DateTime manufactureDate = DateTime.Parse(Console.ReadLine());

                            ListEnums(typeof(Furniture));
                            Console.Write("Furniture: ");
                            Furniture kind = (Furniture)int.Parse(Console.ReadLine());

                            ListEnums(typeof(HomeLocals));
                            Console.Write("Home Local: ");
                            HomeLocals homeLocal = (HomeLocals)int.Parse(Console.ReadLine());
                            list.Add(new Furnitures(name, description, weight,
                            price, kind, homeLocal, manufactureDate));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Wrong imput! {e}. Try again!");
                            nok = true;
                        }
                    } while (nok);
                }
            }

            Console.Clear();
            Console.WriteLine(new string('#', LineSize));
            Console.WriteLine("PRICE TAGS:");
            foreach (Item prod in list)
            {
                Console.WriteLine($"\n{new string('-', LineSize)}");
                Console.WriteLine(prod.PriceTag());
            }

            Console.Write("\nPress any key to close the app.");
            Console.ReadKey();
        }
    }
}
