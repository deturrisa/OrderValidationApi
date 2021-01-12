using System;
using Microsoft.Extensions.Logging;
using OrderValidation.Basket.Validation;
using OrderValidation.Common;
using OrderValidation.Common.Requests;

namespace OrderValidation.Service
{
    public class ValidationService : IValidationService
    {
        private readonly ILogger<ValidationService> _logger;
        private readonly IPortfolioValidationService _portfolioService;

        public ValidationService(ILogger<ValidationService> logger, IPortfolioValidationService portfolioService)
        {
            _logger = logger;
            _portfolioService = portfolioService;
        }

        public OrdersValidationResponse ValidateOrders(string clientId, OrdersRequest request)
        {

            _logger.LogInformation($"Validating orders request for client: {clientId}...");

            var portfolio = new Portfolio(){ request.Stocks };

            _logger.LogInformation("Created portfolio");

            var validationState =_portfolioService.ValidatePortfolio(portfolio, clientId);
                
            _logger.LogInformation("Received validation state ");

            var response = BuildResponse(validationState);
            
            _logger.LogInformation("Validation response retrieved");

            return response;

        }

        private OrdersValidationResponse BuildResponse(ValidationState validationState)
        {
            switch (validationState)
            {
                case ValidationState.Success:
                    return OrdersValidationResponses.Success;
                case ValidationState.InvalidCurrencyFormat:
                    return OrdersValidationResponses.InvalidCurrencyFormat;
                case ValidationState.InvalidWeightState:
                    return OrdersValidationResponses.InvalidPortfolioWeight;
                case ValidationState.EmptyPortfolio:
                    return OrdersValidationResponses.EmptyPortfolio;
                case ValidationState.UnsupportedCurrency:
                    return OrdersValidationResponses.UnsupportedCurrency;
                case ValidationState.UnsupportedClient:
                    return OrdersValidationResponses.ClientIdNotFound;
                case ValidationState.InvalidOrderId:
                    return OrdersValidationResponses.InvalidOrderId;
                case ValidationState.UnsupportedType:
                    return OrdersValidationResponses.UnsupportedType;
                case ValidationState.UnsupportedDestination:
                    return OrdersValidationResponses.UnsupportedDestination;
                case ValidationState.MinimumStockNotionalAmountNotMet:
                    return OrdersValidationResponses.MinimumStockNotionalAmountNotMet;
                case ValidationState.MinimumPortfolioNotionalAmountNotMet:
                    return OrdersValidationResponses.MinimumPortfolioNotionalAmountNotMet;
                case ValidationState.MaximumPortfolioNotionalAmountNotMet:
                    return OrdersValidationResponses.MaximumStockNotionalAmountNotMet;
                case ValidationState.NegativeNotionalWeight:
                    return OrdersValidationResponses.NegativeNotionalAmount;
                case ValidationState.NegativeStockWeight:
                    return OrdersValidationResponses.NegativeStockWeight;
                case ValidationState.InvalidCurrencySymbol:
                    return OrdersValidationResponses.InvalidSymbol;
                default:
                    return OrdersValidationResponses.UnknownError;
            }
        }
    }
}
