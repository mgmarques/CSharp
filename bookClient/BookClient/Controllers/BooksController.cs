using BookClient.Models;
using System.Text.Json;
using System.Text;

namespace BookClient.Controllers
{
    public class BooksController
    {
        private HttpClient Client;
        private int Port;
        private string BaseUri; 

        public BooksController(HttpClient client, int port)
        {
            Client = client;
            Port = port;
            BaseUri = $"https://localhost:{Port}/api/Books";
        }

        // <Delete a Book>
        public async Task DeleteBookAsync(int id)
        {
            using HttpResponseMessage response = await Client.DeleteAsync($"{BaseUri}/{id}");
            
            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response:\n{jsonResponse}\n");
        }
        
        public async Task GetAsync(int id)
        {
            using HttpResponseMessage response = await Client.GetAsync($"{BaseUri}/{id}");
            
            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response:\n{jsonResponse}\n");
        }

        // List all books: api/Books
        public async Task<List<Book>> GetAlltBooksAsync()
        {

            await using Stream stream =
                await Client.GetStreamAsync(BaseUri);

            var books =
                await JsonSerializer.DeserializeAsync<List<Book>>(stream);

            return books ?? new();
        }

        // Get one book by ID: api/Books/5
        public async Task<Book> GetBookAsync(int id)
        {
            await using Stream stream =
                await Client.GetStreamAsync($"{BaseUri}/{id}");

            return await JsonSerializer.DeserializeAsync<Book>(stream);
        }

        // Add a Book
        public async Task PostBookAsync(Object book)
        {
            try
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(book),
                    Encoding.UTF8,
                    "application/json");

                using HttpResponseMessage response = await Client.PostAsync(BaseUri, jsonContent);

                response.EnsureSuccessStatusCode().WriteRequestToConsole();
            
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Book Added:\n{jsonResponse}\n");
            }
            catch(Exception)
            {
                Console.WriteLine($"Error when try add the book:\n {book}");
            }
        }

    // Update a Book
        public async Task PutBookAsync(Object book, int id)
        {
            try
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(book),
                    Encoding.UTF8,
                    "application/json");

                using HttpResponseMessage response = await Client.PutAsync($"{BaseUri}/{id}", jsonContent);

                response.EnsureSuccessStatusCode().WriteRequestToConsole();
                
                var jsonResponse = await response.Content.ReadAsStringAsync();
            }
            catch(Exception)
            {
                Console.WriteLine($"Error when try update the book:\n {book}");
            }
        }
    }
}
