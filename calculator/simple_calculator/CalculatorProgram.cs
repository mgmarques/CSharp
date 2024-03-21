using System.Diagnostics;
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    class CalculatorProgram
    {
        public static void Main(string[] args)
        {
            bool endApp = false;
            Calculator calculator = new Calculator();

            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) 
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                Console.Clear();
                // Display title as the C# console calculator app.
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");
                // Ask the user to type the first number.
                Console.Write("Please, type the firt number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double num1 = 0;
                while (!double.TryParse(numInput1, out num1))
                {
                    Console.Write("This is not valid input. Please enter a valid number: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Please, type the second number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double num2 = 0;
                while (!double.TryParse(numInput2, out num2))
                {
                    Console.Write("This is not valid input. Please enter a valid number: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");
                string? op = Console.ReadKey().Key.ToString().ToLower();
                Console.WriteLine("");

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(num1, num2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' to close the app, or press any other key to continue: ");
                if (Console.ReadKey().Key.ToString().ToLower() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}