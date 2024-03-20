using Xunit.Abstractions;
using ShapesDrawing.Entities;
using ShapesDrawing.Entities.Enums;

namespace ShapesDrawing.Unit.Tests;

public class ShapesDrawingUnitTest
{
    public class ShapesTest
    {

        [Theory]
        [InlineData(30, 40, 10, 15, Color.Red)]
        [InlineData(1, 2, 7, 3, Color.Blue)]
        public void Retangle(int x, int y, double width, double height,
            Color color)
        {
            Rectangle rectangle = new Rectangle(x, y, width, height, color);

            double expected = width * height;
            Assert.Equal(expected, rectangle.Area());
            expected = 2 * (width + height);
            Assert.Equal(expected, rectangle.Perimeter());

        }

        [Theory]
        [InlineData(5, 5, 10, Color.Red)]
        [InlineData(1, 2, 5, Color.Blue)]
        public void CircleTest(int x, int y, double radius, Color color)
        {
            Circle circle = new Circle(x, y, radius, color);

            double expected = Math.PI * radius * radius;
            Assert.Equal(expected, circle.Area());
            expected = 2 * Math.PI * radius;
            Assert.Equal(expected, circle.Perimeter());

        }
    }
}