using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderValidation.Common;

namespace OrderValidation.Currency.Validation
{
    public class CurrencyValidationService : ICurrencyValidationService
    {
        private readonly string[] _supportedCurrencies =
        {
            Currencies.HKD,
            Currencies.USD
        };

        public ValidationState ValidateCurrency(string currency)
        {
            return _supportedCurrencies.Contains(currency) ? ValidationState.Success : ValidationState.UnsupportedCurrency;
        }
    }
}
