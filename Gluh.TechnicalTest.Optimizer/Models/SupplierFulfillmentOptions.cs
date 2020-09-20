using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Models
{
    public class SupplierFulfillmentOptions
    {
        public Supplier Supplier { get; set; }

        public decimal ShippingCost { get; set; }

        public decimal SupplierCost { get; set; }

        public decimal StockOnHand { get; set; }

        public decimal StockAvailableToSupply { get; set; }

        public decimal UnitCostIncludingShipping { get; set; }
    }
}
