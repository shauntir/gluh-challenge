using System;
using Xunit;
using Gluh.TechnicalTest.Services;
using System.Linq;
using Gluh.TechnicalTest;
using Gluh.TechnicalTest.Models;
using System.Collections.Generic;
using Gluh.TechnicalTest.Database;

namespace GluhChallenge.Tests
{
    public class SupplierShippingCostCalculatorTests
    {
        public static List<object[]> SupplierData 
        {
            get 
            {
                return new TestData().Create().SelectMany(x => x.Product.Stock).Select(y => new object[] { y.Supplier }).Distinct().ToList(); 
            }
        }

        [Theory, MemberData(nameof(SupplierData))]
        public void Test_Supplier_Shipping_Calculator(Supplier supplier)
        {
            // Arrange
            var supplierShippingCostCalculator = new SupplierShippingCostCalculator();

            var quantityRequestedOverMax = supplier.ShippingCostMaxOrderValue + 1m;
            var resultOverMax = supplierShippingCostCalculator.CalculateShippingCost(supplier, quantityRequestedOverMax);
            
            var quantityRequestedUnderMin = supplier.ShippingCostMinOrderValue - 1m;
            var resultUnderMin = supplierShippingCostCalculator.CalculateShippingCost(supplier, quantityRequestedUnderMin);
            
            var random = new Random();
            var minRandomValue = supplier.ShippingCostMinOrderValue == 0 ? 0 : supplier.ShippingCostMinOrderValue + 1m;
            var maxRandomValue = supplier.ShippingCostMaxOrderValue == 0 ? 0 : supplier.ShippingCostMaxOrderValue - 1m;
            var randomQuantityBetweenMinAndMax = random.Next((int)minRandomValue, (int)maxRandomValue);

            // Act
            var resultBetweenRange = supplierShippingCostCalculator.CalculateShippingCost(supplier, randomQuantityBetweenMinAndMax);

            // Assert
            Assert.Equal(0m, resultOverMax);
            Assert.Equal(0m, resultUnderMin);
            Assert.Equal(supplier.ShippingCost, resultBetweenRange);
        }
    }
}
