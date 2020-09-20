using Gluh.TechnicalTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gluh.TechnicalTest.Services
{
    public class ServiceTypeFulfillmentService : IFulfillmentService
    {
        public ServiceTypeFulfillmentService()
        {
        }

        public List<PurchaseOrderItem> GetPurchaseOrderItems(List<PurchaseRequirement> purchaseRequirements)
        {
            var result = purchaseRequirements
                .Where(s => s.Product.Type == Database.ProductType.Service)
                .Select(purchaseRequirement => new PurchaseOrderItem
                {
                    PurchaseRequirement = purchaseRequirement,
                    CostToFulfill = 0m,
                    SelfFulfillment = true,
                    QuantityFulfilled = purchaseRequirement.Quantity,
                    SupplierToFulfull = new Database.Supplier { Name = "This company" }
                })
                .ToList();

            return result;
        }
    }
}
