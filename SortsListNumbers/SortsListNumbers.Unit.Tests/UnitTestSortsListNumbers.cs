using Xunit.Abstractions;

namespace SortsListNumbers.Unit.Tests;

public class UnitTestSortsListNumbers
{
    public class SortsListNumbersTest
    {
        private readonly SortsListNumbersApp.App _app;
        private readonly ITestOutputHelper output;

        public SortsListNumbersTest(ITestOutputHelper output)
        {
            _app = new SortsListNumbersApp.App();
            this.output = output;

        }

        [Theory]
        [InlineData(new string[] { "9", "3", "7" }, new int[] { 3, 7, 9 })]
        [InlineData(new string[] { "A", "3", "7" }, new int[] {})]
        [InlineData(new string[] {}, new int[] {})]
        public void SortList(string[] line, int[] expected)
        {
            var result = _app.SortList(line);

            Assert.Equal(expected, result);

            var content = new System.Text.StringBuilder();
            var writer = new StringWriter(content);
        }
    }
}