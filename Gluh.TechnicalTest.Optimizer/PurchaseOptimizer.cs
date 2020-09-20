using System.Collections.Generic;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Services;
using System.Linq;
using System;

namespace Gluh.TechnicalTest
{
    public class PurchaseOptimizer
    {
        private readonly ICollection<IFulfillmentService> _allFulfillmentServices;

        public PurchaseOptimizer(ICollection<IFulfillmentService> allFulfillmentServices)
        {
            _allFulfillmentServices = allFulfillmentServices;
        }

        /// <summary>
        /// Calculates the optimal set of supplier to purchase products from.
        /// </summary>
        public void Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            PrintPurchaseRequirements(purchaseRequirements);

            var purchaseOrderItems = _allFulfillmentServices.SelectMany(x => x.GetPurchaseOrderItems(purchaseRequirements));
            var supplierPurchaserOrders = purchaseOrderItems
                .GroupBy(x => x.SupplierToFulfull)
                .Select(group => new PurchaseOrder
                {
                    Supplier = group.Key,
                    PurchasOrderItems = group.ToList()
                })
                .ToList();

            PrintPurchaseOrders(supplierPurchaserOrders);
        }

        private void PrintPurchaseRequirements(List<PurchaseRequirement> purchaseRequirements)
        {
            Console.WriteLine($"~~~~~~~~~~~~~~~~ Purchase Requirements Section ~~~~~~~~~~~~~~~~");
            foreach (var item in purchaseRequirements)
            {
                Console.WriteLine($"------------- Purchase Requirement -------------");
                Console.WriteLine($"{item.Product.Name} needs to be fulfilled at a quantity of {item.Quantity}");
                Console.WriteLine($"-------------------------------------------------------");
                Console.WriteLine();
            }
        }

        private void PrintPurchaseOrders(List<PurchaseOrder> supplierPurchaserOrders)
        {
            Console.WriteLine($"~~~~~~~~~~~~~~~~ Purchase Order Section ~~~~~~~~~~~~~~~~");
            foreach (var purchaseOrder in supplierPurchaserOrders)
            {
                Console.WriteLine($"------------- Purchase order for Supplier -------------");
                foreach (var item in purchaseOrder.PurchasOrderItems)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine($"-------------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
