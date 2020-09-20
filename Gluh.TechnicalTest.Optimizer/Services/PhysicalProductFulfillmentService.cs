using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gluh.TechnicalTest.Services
{
    public interface IPhysicalProductFulfillmentService
    {
        List<PurchaseRequirementFulfillmentOptions> GetAllPurchaseRequirementFulfillmentOptions(List<PurchaseRequirement> purchaseRequirements);
    }

    public class PhysicalProductFulfillmentService : IPhysicalProductFulfillmentService
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierShippingCostCalculator _supplierShippingCostCalculator;

        public PhysicalProductFulfillmentService(ISupplierService supplierService, ISupplierShippingCostCalculator supplierShippingCostCalculator)
        {
            _supplierService = supplierService;
            _supplierShippingCostCalculator = supplierShippingCostCalculator;
        }

        public List<PurchaseRequirementFulfillmentOptions> GetAllPurchaseRequirementFulfillmentOptions(List<PurchaseRequirement> purchaseRequirements)
        {
            var result = purchaseRequirements
                .Where(s => s.Product.Type == Database.ProductType.Physical)
                .Select(purchaseRequirement => new
                {
                    PurchaseRequirement = purchaseRequirement,
                    SupplierFullfilment = purchaseRequirement.Product.Stock.Select(stock => new
                    {
                        Supplier = stock.Supplier,
                        ShippingCost = _supplierShippingCostCalculator.CalculateShippingCost(stock.Supplier, _supplierService.GetStockQuantityAbleToSupply(stock.StockOnHand, purchaseRequirement.Quantity)),
                        SupplierCost = stock.Cost,
                        StockOnHand = stock.StockOnHand,
                        StockAvailableToSupply = _supplierService.GetStockQuantityAbleToSupply(stock.StockOnHand, purchaseRequirement.Quantity)
                    })
                })
                .Select(supplierStock => new PurchaseRequirementFulfillmentOptions
                {
                    PurchaseRequirement = supplierStock.PurchaseRequirement,
                    OptionsList = supplierStock.SupplierFullfilment.Select(fulfullment => new SupplierFulfillmentOptions
                    {
                        Supplier = fulfullment.Supplier,
                        ShippingCost = fulfullment.ShippingCost,
                        SupplierCost = fulfullment.SupplierCost,
                        StockOnHand = fulfullment.StockOnHand,
                        StockAvailableToSupply = fulfullment.StockAvailableToSupply,
                        UnitCostIncludingShipping = _supplierService.CalculateUnitCostIncludingShipping(fulfullment.StockAvailableToSupply, fulfullment.SupplierCost, fulfullment.ShippingCost)
                    })
                    .OrderBy(x => x.UnitCostIncludingShipping)
                    .ToList()
                })
                .ToList();

            return result;
        }
    }
}
