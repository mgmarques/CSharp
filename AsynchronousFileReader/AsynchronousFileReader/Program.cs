using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AsynchronousFileReader;

class Program
{
    public const int LineSize = 60;

    static async Task<string> ReadFileAsync(string filePath)
    {
        // Encapsulate the read operation in a try-catch block to handle potential exceptions.
        try
        {
            // Use the System.IO.File class and its ReadAllTextAsync method to read the file contents
            return await File.ReadAllTextAsync(filePath);
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException("The file was not found!");
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new DirectoryNotFoundException($"Directory not found! {ex.Message}");
        }
        catch (IOException ex)
        {
            throw new Exception($"{ex.GetType().Name}: An error occurred while reading the file: {ex.Message}");
        }

        
    }

    static async Task Main(string[] args)
    {
        bool endApp = false;
        while (!endApp)
        {
            Console.Clear();
            Console.WriteLine("Asynchronous File Reader:");
            Console.WriteLine("-------------------------");
            string workingDirectory = Environment.CurrentDirectory;
            string project = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string path = System.IO.Path.Join(project, "Resources");
            Console.Write($"Enter the full file name or full filename with its sub folder to read from path {path}: ");
            string fileName = Console.ReadLine();
            string fullPath = System.IO.Path.Join(path, fileName);
            try
            {
                // Use async and await to perform the read operation without blocking the main thread.
                string fileContents = await ReadFileAsync(fullPath);
                Console.WriteLine($"\nReading the {fileName} file from path: {path}");
                Console.WriteLine("\nFile Contents:");
                Console.WriteLine($"{new string('-', LineSize)}");
                Console.WriteLine(fileContents);
                Console.WriteLine($"{new string('-', LineSize)}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }

            // Wait for the user to respond before closing.
            Console.Write("\nPress 'n' to close the app, or press any other key to continue: ");
            if (Console.ReadKey().Key.ToString().ToLower() == "n") endApp = true;
        }
    }
}

