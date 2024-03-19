using Xunit.Abstractions;

namespace Calculator.UnitTests.Services
{
    public class WriterTest
    {
        private readonly TextWriter outputWriter;
        private readonly string message;

        public WriterTest(TextWriter outputWriter, string message)
        {
            this.outputWriter = outputWriter;
            this.message = message;

        }

        public void PrintMessage()
        {
            outputWriter.WriteLine(message);
        }
    }

    public class CalculatorTest
    {
        private readonly CalculatorLibrary.Calculator _calculator;
        private readonly ITestOutputHelper output;

        public CalculatorTest(ITestOutputHelper output)
        {
            _calculator = new CalculatorLibrary.Calculator();
            this.output = output;

        }

        [Theory]
        [InlineData(2.0, 2.0, "a", 4.0, "Add")]
        [InlineData(1.0, 1.0, "s", 0.0, "Subtract")]
        [InlineData(3.0, 2.0, "m", 6.0, "Multiply")]
        [InlineData(3.0, 2.0, "d", 1.5, "Divide")]
        [InlineData(3.0, 0.0, "d", double.NaN, "Error: Division by zero is not allowed.")]
        public void CalculatorDo(double num1, double num2, string op,
            double expected, string message)
        {
            var result = _calculator.DoOperation(num1, num2, op);

            Assert.Equal(expected, result);
            output.WriteLine(message);

            var content = new System.Text.StringBuilder();
            var writer = new StringWriter(content);
            var sut = new WriterTest(writer, message);

            sut.PrintMessage();

            var actualOutput = content.ToString();
            Assert.Equal(message + "\n", actualOutput);
        }
    }
}