using Gluh.TechnicalTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gluh.TechnicalTest.Services
{
    public interface IPurchaseOrderFulfillmentService
    {
        IEnumerable<PurchaseOrderItem> ProducePurchaseOrderItem(List<SupplierFulfillmentOptions> fulfillmentOptions, PurchaseRequirement purchaseRequirement);
    }

    public class PurchaseOrderFulfillmentService : IPurchaseOrderFulfillmentService
    {
        private readonly IStockCalculatorService _stockCalculatorService;

        public PurchaseOrderFulfillmentService(IStockCalculatorService stockCalculatorService)
        {
            _stockCalculatorService = stockCalculatorService;
        }

        public IEnumerable<PurchaseOrderItem> ProducePurchaseOrderItem(List<SupplierFulfillmentOptions> fulfillmentOptions, PurchaseRequirement purchaseRequirement)
        {
            var quantityNeeded = purchaseRequirement.Quantity;

            if (fulfillmentOptions.Count() == 0)
            {
                yield return new PurchaseOrderItem
                {
                    PurchaseRequirement = purchaseRequirement,
                    QuantityFulfilled = quantityNeeded,
                    UnableToFulfill = true,
                    CostToFulfill = 0
                };
            }

            foreach (var option in fulfillmentOptions)
            {
                if (_stockCalculatorService.QuantityLeftToFulfill(quantityNeeded, (int)option.StockAvailableToSupply) == 0)
                {
                    yield return new PurchaseOrderItem
                    {
                        PurchaseRequirement = purchaseRequirement,
                        QuantityFulfilled = quantityNeeded,
                        SupplierToFulfull = option.Supplier,
                        CostToFulfill = (option.SupplierCost * quantityNeeded) + option.ShippingCost
                    };
                    yield break;
                }

                quantityNeeded -= (int)option.StockAvailableToSupply;
                yield return new PurchaseOrderItem
                {
                    PurchaseRequirement = purchaseRequirement,
                    QuantityFulfilled = (int)option.StockAvailableToSupply,
                    SupplierToFulfull = option.Supplier,
                    CostToFulfill = (option.SupplierCost * option.StockAvailableToSupply) + option.ShippingCost
                };
            }
        }
    }
}
