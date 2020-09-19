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
        /// ### Complete this method
        /// </summary>
        public void Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            var suppliers = _supplierService.GetSuppliers(purchaseRequirements);

            var x = suppliers.Select(s => new { s, t = _supplierShippingCostCalculator.CalculateShippingCost(s, 3) });

            var purchaseRequirementWithSuppliers = purchaseRequirements.Select(p => new
            {
                PurchaseRequirement = p,
                OurSuppliers = p.Product.Stock.Select(s => new
                {
                    SupplierName = s.Supplier.Name,
                    ShippingCost = _supplierShippingCostCalculator.CalculateShippingCost(s.Supplier, p.Quantity),
                    UnitByQuantityCost = p.Quantity * s.Cost,
                    TotalCostIncludingShipping = (p.Quantity * s.Cost) + _supplierShippingCostCalculator.CalculateShippingCost(s.Supplier, p.Quantity)
                })
                .OrderBy(p => p.TotalCostIncludingShipping)
                .ToList()
            })
            .ToList();


            var calc = purchaseRequirementWithSuppliers;
        }
    }
}
