using Microsoft.Extensions.Logging;
using OrderValidation.Common;
using OrderValidation.Currency.Validation;
using OrderValidation.ChildOrder.Validation;
using OrderValidation.Common.Extensions;

namespace OrderValidation.Client.Global
{
    public class GlobalValidationService : IGlobalValidationService
    {
        private readonly ILogger<GlobalValidationService> _logger;
        private readonly ICurrencyValidationService _currencyValidationService;
        private readonly IStockIdValidationService _stockValidationService;
        private int _previousIndex;
        public GlobalValidationService(ILogger<GlobalValidationService> logger, ICurrencyValidationService currencyValidationService, IStockIdValidationService stockValidationService)
        {
            _logger = logger;
            _currencyValidationService = currencyValidationService;
            _stockValidationService = stockValidationService;
        }

        public ValidationState ValidatePortfolioHasStock(Portfolio portfolio)
        {
            if (portfolio.Count == 0)
            {
                _logger.LogTrace($"Portfolio contains 0 orders");
                return ValidationState.EmptyPortfolio;
            }
            
            _logger.LogTrace($"Portfolio contains {portfolio.Count} orders");
            
            return ValidationState.Success;
        }

        public ValidationState ValidateStock(Stock stock)
        {
            if (stock.Symbol != "$")
            {
                _logger.LogTrace($"Symbol: {stock.Symbol} not supported");
                return ValidationState.InvalidCurrencySymbol;
            }
            if (stock.NotionalAmount < 0)
            {
                _logger.LogTrace($"Stock Notional Amount = {stock.NotionalAmount} cannot be negative");
                return ValidationState.NegativeNotionalWeight;
            }

            if (stock.Weight < 0)
            {
                _logger.LogTrace($"Stock Weight = {stock.Weight} cannot be negative");
                return ValidationState.NegativeStockWeight;
            }

            var validateCurrencyResponse = _currencyValidationService.ValidateCurrency(stock.Currency);

            if (validateCurrencyResponse != ValidationState.Success)
            {
                _logger.LogTrace($"{stock.Currency} is an invalid currency format");
                return validateCurrencyResponse;
            }

            var validateStockIdResponse = _stockValidationService.ValidateOrderId(stock.OrderId,_previousIndex);

            if (validateStockIdResponse != ValidationState.Success)
            {
                _logger.LogTrace($"{stock.OrderId} is an invalid");
                return validateStockIdResponse;
            }

            _previousIndex = stock.OrderId.GetIndexFromOrderId();

            _logger.LogTrace($"Stock passed Global Validation rules. Details = {stock}");
            return ValidationState.Success;
        }

        public ValidationState ValidateTotalPortfolioWeight(decimal totalWeight)
        {
            return totalWeight != 1 ? ValidationState.InvalidWeightState : ValidationState.Success;
        }
    }
}
