using Gluh.TechnicalTest.Database;
using System.Collections.Generic;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseOrder
    {
        public Supplier Supplier { get; set; }

        public List<PurchaseOrderItem> PurchasOrderItems { get; set; }
    }
}
