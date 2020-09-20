using Gluh.TechnicalTest.Models;
using System.Collections.Generic;

namespace Gluh.TechnicalTest.Services
{
    public interface IFulfillmentService
    {
        List<PurchaseOrderItem> GetPurchaseOrderItems(List<PurchaseRequirement> purchaseRequirements);
    }
}
