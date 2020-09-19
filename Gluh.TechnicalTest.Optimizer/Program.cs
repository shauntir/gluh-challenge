using Gluh.TechnicalTest.Services;
using System;
using System.Collections.Generic;


namespace Gluh.TechnicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var supplierService = new SupplierService();
            var supplierShippingCostCalculator = new SupplierShippingCostCalculator();

            var purchaseRequirements = new TestData().Create();
            var purchaseOptimizer = new PurchaseOptimizer(supplierService, supplierShippingCostCalculator);

            purchaseOptimizer.Optimize(purchaseRequirements);

            Console.WriteLine("App");
            Console.Read();
        }
    }
}
