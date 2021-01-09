using OrderValidation.Common;

namespace OrderValidation.Client.Clients
{
    public interface IClientValidation
    {
        ValidationState ValidateStock(Stock stock);
        ValidationState ValidateTotalPortfolioNotionalAmount(decimal notionalAmountTotal);
    }
}