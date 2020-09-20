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
            return $"Required to fulfill a total of {PurchaseRequirement.Quantity} {PurchaseRequirement.Product.Name} " +
                $"{Environment.NewLine}" +
                $"Supplier {SupplierToFulfull.Name} can fulfill {QuantityFulfilled} at a cost of ${CostToFulfill}" +
                $"{Environment.NewLine}";
        }
    }
}
