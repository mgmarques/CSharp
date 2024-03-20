using ShapesDrawing.Interfaces;


namespace ShapesDrawing.Entities
{
    // Abstract class for shapes
    public abstract class Shape : IDrawable
    {
        protected int x;
        protected int y;
        protected Enums.Color color;


        public Shape(int x, int y, Enums.Color color = Enums.Color.Black)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        // Abstract method for drawing a shape
        // includes a default implementation using the virtual keyword, 
        // indicating that it can be overridden in derived classes. 
        // Code to resize the shape
        public virtual void Draw()
        {
            Console.WriteLine($"Drawing a shape at ({x}, {y} " +
                $"with the color {color} )");
        }

        // Abstract methods for calculate the shape area and the perimeter 
        public abstract double Area();
        public abstract double Perimeter();

    }
}
