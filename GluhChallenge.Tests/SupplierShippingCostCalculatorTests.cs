using System;
using Xunit;
using Gluh.TechnicalTest.Services;
using System.Linq;
using Gluh.TechnicalTest;

namespace GluhChallenge.Tests
{
    public class SupplierShippingCostCalculatorTests
    {
        [Fact]
        public void Test1()
        {
            var purchaseRequirements = new TestData().Create();
            var suppliers = purchaseRequirements.SelectMany(x => x.Product.Stock).Select(y => y.Supplier).Distinct();
            
            var supplier1 = suppliers.First();

            var supplierShippingCostCalculator = new SupplierShippingCostCalculator();
            var result = supplierShippingCostCalculator.CalculateShippingCost(supplier1, 2);
        }
    }
}
