using ShapesDrawing.Entities;
using ShapesDrawing.Entities.Enums;


class Program
{
    static void Main(string[] args)
    {
        // Display title as the C# Shapes Drawing App.
        Console.WriteLine("C# Shapes Drawing App Console:\r");
        Console.WriteLine("------------------------------\n");

        // Creating instances of Circle and Rectangle
        Circle circle = new Circle(10, 20, 5, Color.Blue);
        Rectangle rectangle = new Rectangle(30, 40, 10, 15, Color.Red);

        // Drawing shapes
        circle.Draw();
        rectangle.Draw();

        // Calculate the ashapes reas and perimeter
        Console.WriteLine($"The circle has area {circle.Area()} " +
            $"and the perimeter {circle.Perimeter()}) ");

        Console.WriteLine($"The retangle has area {rectangle.Area()} " +
            $"and the perimeter {rectangle.Perimeter()}) ");

        // Wait for the user to respond before closing.
        Console.WriteLine("\n------------------------\n");
        Console.Write("\nPress any key to close the app.");
        Console.ReadKey();
    }
}
