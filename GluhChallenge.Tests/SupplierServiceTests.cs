using System;
using Xunit;
using Gluh.TechnicalTest.Services;
using System.Linq;
using Gluh.TechnicalTest;
using Gluh.TechnicalTest.Models;
using System.Collections.Generic;
using Xunit.Extensions;
using Gluh.TechnicalTest.Database;

namespace GluhChallenge.Tests
{
    public class SupplierServiceTests
    {
        private readonly List<PurchaseRequirement> _purchaseRequirements;
        public SupplierServiceTests()
        {
            _purchaseRequirements = new TestData().Create();
        }

        [Fact]
        public void Supplier_Service_Tests()
        {
            // Arrange
            var supplierService = new SupplierService();

            // Act
            var result = supplierService.GetSuppliers(_purchaseRequirements);

            // Assert
            Assert.Equal(6, result.Count());
        }
    }
}
