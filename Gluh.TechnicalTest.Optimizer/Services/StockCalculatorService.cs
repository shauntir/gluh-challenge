namespace Gluh.TechnicalTest.Services
{
    public interface IStockCalculatorService
    {
        int QuantityLeftToFulfill(int currentQuantityToFulfill, int stockAvailableToFulfill);
    }

    public class StockCalculatorService : IStockCalculatorService
    {
        public int QuantityLeftToFulfill(int currentQuantityToFulfill, int stockAvailableToFulfill)
        {
            if (currentQuantityToFulfill - stockAvailableToFulfill <= 0)
            {
                return 0;
            }

            return currentQuantityToFulfill - stockAvailableToFulfill;
        }
    }
}
