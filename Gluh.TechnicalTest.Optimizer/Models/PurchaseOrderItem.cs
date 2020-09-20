using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseOrderItem
    {
        public Supplier SupplierToFulfull { get; set; }

        public decimal CostToFulfill { get; set; }

        public bool SelfFulfillment { get; set; }

        public int QuantityFulfilled { get; set; }

        public bool UnableToFulfill { get; set; }

        public PurchaseRequirement PurchaseRequirement { get; set; }

        public override string ToString()
        {
            if (SelfFulfillment)
            {
                return $"'This company' can fulfill {QuantityFulfilled} of {PurchaseRequirement.Quantity} for {PurchaseRequirement.Product.Name} at a cost of ${CostToFulfill}";
            }

            if (UnableToFulfill)
            {
                return $"Unable to fulfill {PurchaseRequirement.Quantity} items for {PurchaseRequirement.Product.Name} due to insufficient stock level";
            }

            return $"{SupplierToFulfull.Name} can fulfill {QuantityFulfilled} of {PurchaseRequirement.Quantity} for {PurchaseRequirement.Product.Name} at a cost of ${CostToFulfill}";
        }
    }
}
