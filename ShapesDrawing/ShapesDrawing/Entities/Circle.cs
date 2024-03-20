using ShapesDrawing.Entities.Enums;


namespace ShapesDrawing.Entities
{
    // Concrete class for Circle
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(int x, int y, double radius, Color color) : base(x, y, color)
        {
            Radius = radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing a {color} circle at ({x}, {y}) " +
                $"with radius {Radius}");
        }

        public override double Area()
        {
            return Math.PI * Radius * Radius;
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * Radius;
        }
    }
}
