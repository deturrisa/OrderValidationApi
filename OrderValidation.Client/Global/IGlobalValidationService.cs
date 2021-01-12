using OrderValidation.Common;

namespace OrderValidation.Client.Global
{
    public interface IGlobalValidationService
    {
        ValidationState ValidatePortfolioHasStock(Portfolio portfolios);
        ValidationState ValidateStock(Stock stock);
        ValidationState ValidateTotalPortfolioWeight(decimal totalWeight);
    }
}
