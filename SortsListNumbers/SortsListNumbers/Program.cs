using SortsListNumbersApp;

namespace SortsListNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# Console Sort a List of Numbers app.
            Console.WriteLine("Console Sort a List of Numbers C#\r");
            Console.WriteLine("---------------------------------\n");
            App sortsListNumbers = new App();

            while (!endApp)
            {
                string[] line = Console.ReadLine().Split(',');
                int[] results = sortsListNumbers.SortList(line);

                // Wait for the user to respond before closing.
                Console.Write("\nPress 'n' to close the app, or press any " +
                    "other key to continue:");
                if (Console.ReadKey().Key.ToString().ToLower() == "n")
                    endApp = true;
                Console.WriteLine("\n------------------------\n");
            }
            return;
        }
    }
}