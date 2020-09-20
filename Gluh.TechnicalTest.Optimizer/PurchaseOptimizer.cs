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
            var purchaseOrderItems = _allFulfillmentServices.SelectMany(x => x.GetPurchaseOrderItems(purchaseRequirements));
            var supplierPurchaserOrders = purchaseOrderItems
                .GroupBy(x => x.SupplierToFulfull)
                .Select(group => new PurchaseOrder
                {
                    Supplier = group.Key,
                    PurchasOrderItems = group.ToList()
                })
                .ToList();


            foreach (var purchaseOrder in supplierPurchaserOrders)
            {
                Console.WriteLine($"Purchase order for {purchaseOrder.Supplier.Name}");
                foreach (var item in purchaseOrder.PurchasOrderItems)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
            }
        }
    }
}
