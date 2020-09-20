using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseOrder
    {
        public Supplier SupplierToFulfull { get; set; }

        public Product ProductFulfilled { get; set; }

        public decimal TotalCost { get; set; }
    }
}
