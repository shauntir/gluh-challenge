using Gluh.TechnicalTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gluh.TechnicalTest.Services
{
    public class PhysicalProductFulfillmentService : IFulfillmentService
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierShippingCostCalculator _supplierShippingCostCalculator;

        public PhysicalProductFulfillmentService(ISupplierService supplierService, ISupplierShippingCostCalculator supplierShippingCostCalculator)
        {
            _supplierService = supplierService;
            _supplierShippingCostCalculator = supplierShippingCostCalculator;
        }

        private int QuantityLeftToFulfill(int currentQuantityToFulfill, int stockAvailableToFulfill)
        {
            if (currentQuantityToFulfill - stockAvailableToFulfill <= 0)
            {
                return 0;
            }

            return currentQuantityToFulfill - stockAvailableToFulfill;
        }

        private IEnumerable<PurchaseOrderItem> ProducePurchaseOrderItem(int quantityNeeded, List<SupplierFulfillmentOptions> fulfillmentOptions, PurchaseRequirement purchaseRequirement)
        {
            foreach (var option in fulfillmentOptions)
            {
                if (QuantityLeftToFulfill(quantityNeeded, (int)option.StockAvailableToSupply) == 0)
                {
                    yield return new PurchaseOrderItem { PurchaseRequirement = purchaseRequirement, QuantityFulfilled = quantityNeeded, SelfFulfillment = false, SupplierToFulfull = option.Supplier, CostToFulfill = (option.SupplierCost * quantityNeeded) + option.ShippingCost };
                    yield break;
                }
                quantityNeeded -= (int)option.StockAvailableToSupply;
                yield return new PurchaseOrderItem { PurchaseRequirement = purchaseRequirement, QuantityFulfilled = (int)option.StockAvailableToSupply, SelfFulfillment = false, SupplierToFulfull = option.Supplier, CostToFulfill = (option.SupplierCost * option.StockAvailableToSupply) + option.ShippingCost };
            }
        }

        public List<PurchaseOrderItem> GetPurchaseOrderItems(List<PurchaseRequirement> purchaseRequirements)
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
                .SelectMany(requirementOption =>
                    ProducePurchaseOrderItem(requirementOption.PurchaseRequirement.Quantity, requirementOption.OptionsList, requirementOption.PurchaseRequirement)
                )
                .ToList();

            return result;
        }
    }
}
