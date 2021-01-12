using OrderValidation.Common;
using OrderValidation.Currency.Validation;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientValidation : ICurrencyValidationService
    {
        ValidationState ValidateStock(Stock stock);
        ValidationState ValidatePortfolioNotionalAmount(decimal amount);
    }
}