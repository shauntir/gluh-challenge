using Gluh.TechnicalTest.Services;
using System;
using System.Collections.Generic;

namespace Gluh.TechnicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Our data store
            var purchaseRequirements = new TestData().Create();

            // Dependencies at top of composition root
            var supplierService = new SupplierService();
            var supplierShippingCostCalculator = new SupplierShippingCostCalculator();
            var stockCalculatorService = new StockCalculatorService();
            var purchaseOrderFulfillmentService = new PurchaseOrderFulfillmentService(stockCalculatorService);

            var physicalProductFulfillmentService = new PhysicalProductFulfillmentService(supplierService, supplierShippingCostCalculator, purchaseOrderFulfillmentService);
            var serviceTypeFulfillmentService = new ServiceTypeFulfillmentService();
            
            // Optimize and generate purchas orders for suppliers to fulfill
            var purchaseOptimizer = new PurchaseOptimizer(new List<IFulfillmentService>() { physicalProductFulfillmentService, serviceTypeFulfillmentService });
            purchaseOptimizer.Optimize(purchaseRequirements);

            Console.Read();
        }
    }
}
