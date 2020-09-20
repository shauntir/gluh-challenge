using Gluh.TechnicalTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gluh.TechnicalTest.Services
{
    public class NonPhysicalProductFulfillmentService : IFulfillmentService
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierShippingCostCalculator _supplierShippingCostCalculator;

        public NonPhysicalProductFulfillmentService(ISupplierService supplierService, ISupplierShippingCostCalculator supplierShippingCostCalculator)
        {
            _supplierService = supplierService;
            _supplierShippingCostCalculator = supplierShippingCostCalculator;
        }

        public List<PurchaseOrderItem> GetPurchaseOrderItems(List<PurchaseRequirement> purchaseRequirements)
        {
            var result = MapPurchaseRequirementToPurchaseRequirementFulfillmentOptions(purchaseRequirements);

            var o = new List<PurchaseOrderItem>();
            foreach (var purchaseRequirementFulfillment in result)
            {
                var quantityNeeded = purchaseRequirementFulfillment.PurchaseRequirement.Quantity;
                foreach (var option in purchaseRequirementFulfillment.OptionsList)
                {
                    if (quantityNeeded - (int)option.StockAvailableToSupply <= 0)
                    {
                        o.Add(new PurchaseOrderItem { PurchaseRequirement = purchaseRequirementFulfillment.PurchaseRequirement, QuantityFulfilled = quantityNeeded,  SelfFulfillment = false, SupplierToFulfull = option.Supplier, CostToFulfill = (option.SupplierCost * quantityNeeded) + option.ShippingCost });
                        break;
                    }
                    quantityNeeded -= (int)option.StockAvailableToSupply;
                    o.Add(new PurchaseOrderItem { PurchaseRequirement = purchaseRequirementFulfillment.PurchaseRequirement, QuantityFulfilled = (int)option.StockAvailableToSupply,SelfFulfillment = false, SupplierToFulfull = option.Supplier, CostToFulfill = (option.SupplierCost * option.StockAvailableToSupply) + option.ShippingCost });
                }
            }

            //var me = result.Select(options => new PurchaseOrderItem
            //{
            //    PurchaseRequirement = options.PurchaseRequirement,
            //    SelfFulfillment = false,
            //    opt

            //})

            var sample = o.Where(x => x.PurchaseRequirement.Product.ID == 5).ToList();

            return new List<PurchaseOrderItem>() { new PurchaseOrderItem() };
        }

        private List<PurchaseRequirementFulfillmentOptions> MapPurchaseRequirementToPurchaseRequirementFulfillmentOptions(List<PurchaseRequirement> purchaseRequirements)
        {
            var result = purchaseRequirements
                .Where(s => s.Product.Type == Database.ProductType.NonPhysical)
                .Select(purchaseRequirement => new
                {
                    PurchaseRequirement = purchaseRequirement,
                    SupplierFullfilment = purchaseRequirement.Product.Stock.Select(stock => new
                    {
                        Supplier = stock.Supplier,
                        ShippingCost = 0,
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
