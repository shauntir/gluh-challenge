using Xunit;
using Gluh.TechnicalTest.Services;
using Gluh.TechnicalTest;
using System.Linq;

namespace GluhChallenge.Tests
{
    public class ProductFulfillmentServiceTests
    {
        [Fact]
        public void ProductFulfillmentServiceTests_Tests()
        {
            // Arrange
            var supplierService = new SupplierService();
            var supplierShippingCostCalculator = new SupplierShippingCostCalculator();
            var stockCalculatorService = new StockCalculatorService();
            var purchaseOrderFulfillmentService = new PurchaseOrderFulfillmentService(stockCalculatorService);

            var productFulfillmentService = new ProductFulfillmentService(supplierService, supplierShippingCostCalculator, purchaseOrderFulfillmentService);

            // Act
            var result = productFulfillmentService.GetPurchaseOrderItems(new TestData().Create());

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.False(item.SelfFulfillment));
        }
    }
}
