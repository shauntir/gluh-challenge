using System.Collections.Generic;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Services;
using System.Linq;

namespace Gluh.TechnicalTest
{
    public class PurchaseOptimizer
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierShippingCostCalculator _supplierShippingCostCalculator;

        public PurchaseOptimizer(ISupplierService supplierService, ISupplierShippingCostCalculator supplierShippingCostCalculator)
        {
            _supplierService = supplierService;
            _supplierShippingCostCalculator = supplierShippingCostCalculator;
        }

        /// <summary>
        /// Calculates the optimal set of supplier to purchase products from.
        /// </summary>
        public void Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            var purchaseRequirementWithSuppliers = purchaseRequirements.Select(p => new
            {
                PurchaseRequirement = p,
                SupplierFullfilment = p.Product.Stock.Select(s => new
                {
                    QuantityRequested = p.Quantity,
                    ProductName = p.Product.Name,
                    ProductType = p.Product.Type,
                    SupplierName = s.Supplier.Name,
                    ShippingCost = _supplierShippingCostCalculator.CalculateShippingCost(s.Supplier, GetStockQuantityAbleToSupply(s.StockOnHand, p.Quantity)),
                    SupplierCost = s.Cost,
                    TotalCostIncludingShipping = (p.Quantity * s.Cost) + _supplierShippingCostCalculator.CalculateShippingCost(s.Supplier, p.Quantity),
                    StockOnHand = s.StockOnHand,
                    StockAvailableToSupply = GetStockQuantityAbleToSupply(s.StockOnHand, p.Quantity)
                })
            })
            .Select(s => new 
            {
                PurchaseRequirement = s.PurchaseRequirement,
                SupplierFullfilment = s.SupplierFullfilment.Select(x => new 
                {
                    QuantityRequested = x.QuantityRequested,
                    ProductName = x.ProductName,
                    ProductType = x.ProductType,
                    SupplierName = x.SupplierName,
                    ShippingCost = x.ShippingCost,
                    SupplierCost = x.SupplierCost,
                    StockOnHand = x.StockOnHand,
                    StockAvailableToSupply = x.StockAvailableToSupply,
                    UnitCostIncludingShipping = ((x.StockAvailableToSupply * x.SupplierCost) + x.ShippingCost) / (x.StockAvailableToSupply == 0 ? 1 : x.StockAvailableToSupply)
                }).ToList()
            })
            .ToList();


            var calc = purchaseRequirementWithSuppliers;
        }

        private decimal GetStockQuantityAbleToSupply(decimal stockOnHand, decimal quantityRequested)
        {
            return stockOnHand < quantityRequested ? stockOnHand : quantityRequested;
        }
    }
}
