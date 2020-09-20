using System.Collections.Generic;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Services;
using System.Linq;

namespace Gluh.TechnicalTest
{
    public class PurchaseOptimizer
    {
        private readonly IPhysicalProductFulfillmentService _physicalProductFulfillmentService;

        public PurchaseOptimizer(IPhysicalProductFulfillmentService physicalProductFulfillmentService)
        {
            _physicalProductFulfillmentService = physicalProductFulfillmentService;
        }

        /// <summary>
        /// Calculates the optimal set of supplier to purchase products from.
        /// </summary>
        public void Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            var result = _physicalProductFulfillmentService.GetAllPurchaseRequirementFulfillmentOptions(purchaseRequirements);

            
        }
    }
}
