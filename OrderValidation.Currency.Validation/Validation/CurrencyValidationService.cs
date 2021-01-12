using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OrderValidation.Common;

namespace OrderValidation.Currency.Validation
{
    public class CurrencyValidationService : ICurrencyValidationService
    {
        private readonly ILogger<CurrencyValidationService> _logger;

        public CurrencyValidationService(ILogger<CurrencyValidationService> logger)
        {
            _logger = logger;
        }

        private readonly string[] _supportedCurrencies =
        {
            SupportedCurrencies.HKD,
            SupportedCurrencies.USD
        };

        public ValidationState ValidateCurrency(string currency)
        {
            _logger.LogTrace($"Attempting to validate currency {currency}");
            
            if (!Regex.Match(currency, @"[A-Z]{3}").Success)
            {
                _logger.LogWarning($"{currency} is invalid");
                
                return ValidationState.InvalidCurrencyFormat;
            }

            if (!_supportedCurrencies.Contains(currency))
            {
                _logger.LogWarning($"{currency} not supported");
                return ValidationState.UnsupportedCurrency;
            }

            _logger.LogInformation($"{currency} validation succeeded");
            
            return ValidationState.Success;
        }
    }
}
