using Xunit;
using Gluh.TechnicalTest.Services;
using Gluh.TechnicalTest;
using System.Linq;

namespace GluhChallenge.Tests
{
    public class ServiceTypeFulfillmentServiceTests
    {
        [Fact]
        public void ServiceTypeFulfillmentService_Tests()
        {
            // Arrange
            var serviceTypeFulfillmentService = new ServiceTypeFulfillmentService();

            // Act
            var result = serviceTypeFulfillmentService.GetPurchaseOrderItems(new TestData().Create());

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item.SelfFulfillment));
        }
    }
}
