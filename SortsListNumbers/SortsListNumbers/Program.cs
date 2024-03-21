using SortsListNumbersApp;

namespace SortsListNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            App sortsListNumbers = new App();

            while (!endApp)
            {
                // Display title as the C# Console Sort a List of Numbers app.
                Console.WriteLine("Console Sort a List of Numbers C#\r");
                Console.WriteLine("---------------------------------\n");
                Console.WriteLine("Enter the list of comma-separated integers below:\n");
                string[] line = Console.ReadLine().Split(',');
                int[] results = sortsListNumbers.SortList(line);

                // Wait for the user to respond before closing.
                Console.Write("\nPress 'n' to close the app, or press any " +
                    "other key to continue:");
                if (Console.ReadKey().Key.ToString().ToLower() == "n")
                    endApp = true;
                Console.Clear();
            }
            return;
        }
    }
}