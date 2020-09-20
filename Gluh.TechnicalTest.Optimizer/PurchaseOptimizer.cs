using System.Collections.Generic;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Services;
using System.Linq;
using System;

namespace Gluh.TechnicalTest
{
    public class PurchaseOptimizer
    {
        private readonly IFulfillmentService _physicalProductFulfillmentService;

        public PurchaseOptimizer(IFulfillmentService physicalProductFulfillmentService)
        {
            _physicalProductFulfillmentService = physicalProductFulfillmentService;
        }

        /// <summary>
        /// Calculates the optimal set of supplier to purchase products from.
        /// </summary>
        public void Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            var result = _physicalProductFulfillmentService.GetPurchaseOrderItems(purchaseRequirements);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
