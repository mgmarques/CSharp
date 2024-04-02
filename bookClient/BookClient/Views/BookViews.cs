using System;
using BookClient.Models;

namespace BookClient.Views
{
	public static class BookViews
	{
        const int LineSize = 120;
        const string pattern = "MM-dd-yyyy";

        public static void Welcome(string appName = "Book Collection")
        {
            BookViews.Title($"Welcome to the {appName} Application:", true);
            Console.WriteLine("\nImportant Note:");
            Console.WriteLine(  "---------------");
            Console.WriteLine("\nYou must first run the bookApi project for the BookAPI to be available for this project.");
            Console.WriteLine("After 'dotnet run', remember to note the bookApi port, for example:\n"
                            + "     info: Microsoft.Hosting.Lifetime[14]\n"
                            + "     Now listening on: https://localhost:7228\n\n");
        }

        public static void WaintForUser(string action = "continue")
        {
            Console.WriteLine($"\n\n{new string('-', (LineSize) + 20)}");
            Console.WriteLine($"Press any key to {action}...");
            Console.ReadKey();
        }

        public static void Title(string title, bool clean = false)
        {
            if(clean)
            {
                Console.Clear();
            }
            Console.WriteLine($"{new string('-', (LineSize) + 20)}");
            Console.WriteLine(title);
            Console.WriteLine($"{new string('_', (LineSize) + 20)}\n");
        }

        public static void PrintBook(Book book)
        {
            Console.WriteLine($"ID: {book.ID}");
            Console.WriteLine($"{new string('-', LineSize)}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Category: {book.Category}");
            Console.WriteLine($"Publisher: {book.Publisher}");
            Console.WriteLine($"Edition: {book.Edition} - Edition Date: {book.EditionDate.ToString(pattern)}");
            Console.WriteLine($"Language: {book.Language}");
            Console.WriteLine($"Paperback: {book.PaperbackPages} pages");
            Console.WriteLine($"ISBN_10: {book.ISBN_10}   -   ISBN_13: {book.ISBN_13}");
            Console.WriteLine($"Weight: {book.Weight}\n");
        }
    }
}


