using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Services
{
    public interface ISupplierShippingCostCalculator 
    {
        decimal CalculateShippingCost(Supplier supplier, int quantityRequested);
    }
    public class SupplierShippingCostCalculator : ISupplierShippingCostCalculator
    {
        public decimal CalculateShippingCost(Supplier supplier, int quantityRequested)
        {
            if (supplier.ShippingCostMinOrderValue >= quantityRequested && supplier.ShippingCostMaxOrderValue <= quantityRequested)
            {
                return supplier.ShippingCost;
            }
            return 0m;
        }
    }
}