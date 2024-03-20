using ShapesDrawing.Entities.Enums;


namespace ShapesDrawing.Entities
{
    // Concrete class for Rectangle
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(int x, int y, double width, double height, Color color) : base(x, y, color)
        {
            Width = width;
            Height = height;
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing a {color} rectangle at ({x}, {y}) " +
                $"with width {Width} and height {Height}");
        }

        public override double Area()
        {
            return Width * Height;
        }

        public override double Perimeter()
        {
            return 2 * (Width + Height);
        }
    }
}
