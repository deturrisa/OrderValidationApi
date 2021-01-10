using OrderValidation.Common;
using OrderValidation.Common.Type;
using OrderValidation.Currency.Validation;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientValidation : ICurrencyValidationService
    {
        ValidationState ValidateType(OrderType orderType);
        ValidationState ValidateStock(Stock stock);
        ValidationState ValidatePortfolioNotionalAmount(decimal notionalAmountTotal); //TODO move this to b and c
        ValidationState ValidateDestination(string destination);
    }
}