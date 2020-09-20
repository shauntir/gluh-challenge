using Gluh.TechnicalTest.Services;
using System;
using System.Collections.Generic;


namespace Gluh.TechnicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var purchaseRequirements = new TestData().Create();

            var supplierService = new SupplierService();
            var supplierShippingCostCalculator = new SupplierShippingCostCalculator();
            var stockCalculatorService = new StockCalculatorService();
            var purchaseOrderFulfillmentService = new PurchaseOrderFulfillmentService(stockCalculatorService);

            var physicalProductFulfillmentService = new PhysicalProductFulfillmentService(supplierService, supplierShippingCostCalculator, purchaseOrderFulfillmentService);
            var serviceTypeFulfillmentService = new ServiceTypeFulfillmentService();
            

            var purchaseOptimizer = new PurchaseOptimizer(physicalProductFulfillmentService);
            purchaseOptimizer.Optimize(purchaseRequirements);

            Console.WriteLine("App");
            Console.Read();
        }
    }
}
