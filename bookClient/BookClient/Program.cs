using System.Net.Http.Headers;
using System.Text.Json;
using BookClient.Models;
using BookClient.Views;
using BookClient.Controllers;
using System.Text;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

bool wrongPort = false;
int port = 7228;
int id;

do
{
    bool nok = true;
    BookViews.Welcome();
    wrongPort = false;

    Console.Write("\nEnter the number of the Books API port: ");
    do
    {
        try
        {
            port = int.Parse(Console.ReadLine());
            nok = false;
        }
        catch (Exception)
        {
            Console.Clear();
            Console.Write("Please, enter a valid integer number! ");
        }    
    } while (nok);

    var cleanController = new BooksController(client, port);

    // Clean up the base throgh DeleteAsync
    try
    {
        for (int i = 1; i < 4; i++)
        {
            await cleanController.DeleteBookAsync(i);
        }
    }
    catch (HttpRequestException ex)
    {
        Console.Clear();
        string er = ex.HttpRequestError.ToString();
        bool inact = new string[] { "ConnectionError", "SecureConnectionError" }.Any(s=>er.Contains(s));
        if(inact)
        {
            Console.WriteLine($"You entered the wrong port {port}, please review and enter the correct Book API port number! ");
            wrongPort = true;
            BookViews.WaintForUser();
        }
        else 
        {
            wrongPort = false;
        }

        if(er=="Unknown")
        {
            Console.WriteLine("The database is empty!");
            BookViews.WaintForUser();
        }
    }
} while (wrongPort);

var booksController = new BooksController(client, port);

// POST Books:
BookViews.Title("Add some Books to the Collection:", true);
var bookToPostAsync = new
{
    id = 1,
    category = 23,
    title = "Apps and Services with .NET 8: Build practical projects with Blazor, .NET MAUI, gRPC, GraphQL, and other enterprise technologies",
    publisher = "Packt Publishing",
    edition = 2,
    editionDate = "2023-12-12",
    language = 0,
    paperbackPages = 798,
    isbN_10 = "183763713X",
    isbN_13 = "978-183763713X",
    weight = 3.01,
    dimensions = "9.25 x 7.52 x 1.59"
};
await booksController.PostBookAsync(bookToPostAsync);

bookToPostAsync = new
{
    id = 2,
    category = 23,
    title = "Blazor 8.0: Moderne Webanwendungen und hybride Cross-Platform-Apps mit .NET 8.0, C# 12.0 und Visual Studio 2022 (.NET 8.0-Fachbuchreihe von Dr. Holger Schwichtenberg 3)",
    publisher = "IT Visions",
    edition = 8,
    editionDate = "2023-07-23",
    language = 7,
    paperbackPages = 1647,
    isbN_10 = "183763713X",
    isbN_13 = "ASN-B0CLKZJKTX",
    weight = 5.75,
    dimensions = "9.25 x 7.52 x 3.09"
};
await booksController.PostBookAsync(bookToPostAsync);

bookToPostAsync = new
{
    id = 3,
    category = 48,
    title = "The Hierarchy of Energy in Architecture: Emergy Analysis",
    publisher = "Routledge",
    edition = 1,
    editionDate = "2015-06-12",
    language = 0,
    paperbackPages = 167,
    isbN_10 = "1138803537",
    isbN_13 = "ASN-1138803537",
    weight = 1.75,
    dimensions = "8.00 x 6.00 x 1.10"
};
await booksController.PostBookAsync(bookToPostAsync);

BookViews.WaintForUser();

// GET all the books in the database
var books = await booksController.GetAlltBooksAsync();

BookViews.Title("List of All Books Available:", true);

foreach (var book in books ?? Enumerable.Empty<Book>())
{
    BookViews.PrintBook(book);
}
BookViews.WaintForUser();

// Get the book with Id 2 before update it
id = 2;
BookViews.Title($"Get the Book Id {id} before update it:", true);
try
{
    await booksController.GetAsync(id);
}
catch(HttpRequestException ex)
{
    Console.WriteLine($"{ex.StatusCode} the Book {id} - {ex.Message}");
}

// PutAsync to update Book Id 2
BookViews.Title($"Update the Book Id {id}:", false);
var bookToPutAsync = new
{
    id = 2,
    category = 23,
    title = "Blazor 8.0: Moderne Webanwendungen und hybride Cross-Platform-Apps mit .NET 8.0, C# 12.0 und Visual Studio 2022",
    publisher = "IT Visions",
    edition = 9,
    editionDate = "2024-01-21",
    language = 7,
    paperbackPages = 1731,
    isbN_10 = "183763714X",
    isbN_13 = "ASN-183763714X",
    weight = 5.80,
    dimensions = "9.20 x 7.50 x 3.10"
};
await booksController.PutBookAsync(bookToPutAsync, id);

BookViews.WaintForUser();

// Get the book with Id 2 after update it
BookViews.Title($"Get the Book Id {id} Details:", true);
try
{
    Book b = await booksController.GetBookAsync(id);
    BookViews.PrintBook(b);
}
catch(HttpRequestException ex)
{
    Console.WriteLine($"{ex.StatusCode} the Book {id} - {ex.Message}");
}

BookViews.WaintForUser("stop the application");
