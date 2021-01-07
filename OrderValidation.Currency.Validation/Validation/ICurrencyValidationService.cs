namespace OrderValidation.Currency.Validation
{
    public interface ICurrencyValidationService
    {
        bool IsSupportedCurrency(string currency);
    }
}