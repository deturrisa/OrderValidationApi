using OrderValidation.Common;

namespace OrderValidation.Currency.Validation
{
    public interface ICurrencyValidationService
    {
        ValidationState ValidateCurrency(string currency);
    }
}