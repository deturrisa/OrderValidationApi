using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderValidation.Currency.Validation
{
    public class CurrencyValidationService : ICurrencyValidationService
    {
        private readonly string[] _supportedCurrencies =
        {
            Currencies.HKD,
            Currencies.USD
        };
        public bool IsSupportedCurrency(string currency)
        {
            return _supportedCurrencies.Contains(currency);
        }
    }
}
