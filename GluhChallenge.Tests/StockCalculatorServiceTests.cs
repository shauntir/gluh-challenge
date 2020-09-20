using Xunit;
using Gluh.TechnicalTest.Services;

namespace GluhChallenge.Tests
{
    public class StockCalculatorServiceTests
    {
        [Theory]
        [InlineData(10, 11, 0)]
        [InlineData(10, 5000, 0)]
        [InlineData(11, 10, 1)]
        [InlineData(5000, 10, 4990)]
        [InlineData(0, 0, 0)]
        public void Stock_Calculator_Tests(int currentQuantityToFulfill, int stockAvailableToFulfill, int expected)
        {
            // Arrange
            var stockCalculatorService = new StockCalculatorService();

            // Act
            var result = stockCalculatorService.QuantityLeftToFulfill(currentQuantityToFulfill, stockAvailableToFulfill);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
