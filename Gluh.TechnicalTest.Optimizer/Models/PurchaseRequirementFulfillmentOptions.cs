using System.Collections.Generic;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseRequirementFulfillmentOptions
    {
        public List<SupplierFulfillmentOptions> OptionsList { get; set; }
        public PurchaseRequirement PurchaseRequirement { get; set; }
    }
}
