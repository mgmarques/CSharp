using Xunit;
using CalculatorLibrary;

namespace Calculator.UnitTests.Services
{
    public class PrimeService_IsPrimeShould
    {
        private readonly CalculatorLibrary.Calculator _primeService;

        public PrimeService_IsPrimeShould()
        {
            _primeService = new CalculatorLibrary.Calculator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var result = _primeService.IsPrime(value);

            Assert.False(result, $"{value} should not be prime");
        }
    }
}