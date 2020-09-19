using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gluh.TechnicalTest.Services
{
    public interface ISupplierService
    {
        List<Supplier> GetSuppliers(List<PurchaseRequirement> purchaseRequirements);
    }

    public class SupplierService : ISupplierService
    {
        public List<Supplier> GetSuppliers(List<PurchaseRequirement> purchaseRequirements)
        {
            return purchaseRequirements.SelectMany(x => x.Product.Stock).Select(y => y.Supplier).Distinct().ToList();
        }
    }
}
