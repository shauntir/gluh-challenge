using System;

namespace Gluh.TechnicalTest.Services
{
    public interface ISupplierService
    {
        decimal GetStockQuantityAbleToSupply(decimal stockOnHand, decimal quantity);

        decimal CalculateUnitCostIncludingShipping(decimal stockAvailableToSupply, decimal supplierCost, decimal shippingCost);
    }

    public class SupplierService : ISupplierService
    {
        public decimal GetStockQuantityAbleToSupply(decimal stockOnHand, decimal quantityRequested)
        {
            return Math.Min(stockOnHand, quantityRequested);
        }

        public decimal CalculateUnitCostIncludingShipping(decimal stockAvailableToSupply, decimal supplierCost, decimal shippingCost)
        {
            return ((stockAvailableToSupply * supplierCost) + shippingCost) / stockAvailableToSupply;
        }
    }
}
