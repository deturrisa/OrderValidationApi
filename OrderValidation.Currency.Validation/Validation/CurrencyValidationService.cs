using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using OrderValidation.Common;

namespace OrderValidation.Currency.Validation
{
    public class CurrencyValidationService : ICurrencyValidationService
    {
        private readonly string[] _supportedCurrencies =
        {
            SupportedCurrencies.HKD,
            SupportedCurrencies.USD
        };

        public ValidationState ValidateCurrency(string currency)
        {
            if (!Regex.Match(currency, @"[A-Z]{3}").Success)
            {
                return ValidationState.InvalidCurrencyFormat;
            }

            return !_supportedCurrencies.Contains(currency) ? ValidationState.UnsupportedCurrency : ValidationState.Success;
        }
    }
}
