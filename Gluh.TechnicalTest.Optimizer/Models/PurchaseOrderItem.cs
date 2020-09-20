using Gluh.TechnicalTest.Database;
using System;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseOrderItem
    {
        public Supplier SupplierToFulfull { get; set; }

        public decimal CostToFulfill { get; set; }

        public bool SelfFulfillment { get; set; }

        public int QuantityFulfilled { get; set; }

        public PurchaseRequirement PurchaseRequirement { get; set; }

        public override string ToString()
        {
            return $"{SupplierToFulfull.Name} can fulfill {QuantityFulfilled} of {PurchaseRequirement.Quantity} for {PurchaseRequirement.Product.Name} at a cost of ${CostToFulfill}";
        }
    }
}
