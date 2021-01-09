using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using OrderValidation.Client;
using OrderValidation.Client.Clients;
using OrderValidation.Client.Global;
using OrderValidation.Common;

namespace OrderValidation.Basket.Validation
{
    public class PortfolioValidationService : IPortfolioValidationService
    {
        private readonly ILogger<PortfolioValidationService> _logger;
        private readonly IClientValidationFactory _clientValidationFactory;
        private readonly IGlobalValidationService _globalValidationService;

        public PortfolioValidationService(ILogger<PortfolioValidationService> logger, IClientValidationFactory clientValidationFactory, IGlobalValidationService globalValidationService)
        {
            _logger = logger;
            _clientValidationFactory = clientValidationFactory;
            _globalValidationService = globalValidationService;
        }

        public ValidationState ValidatePortfolio(Portfolio portfolio, string clientId)
        {
            _logger.LogTrace("Attempting to validate portfolio");

            IClientValidation clientValidation;

            try
            {
                clientValidation = _clientValidationFactory.GetClientValidation(clientId);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogWarning($"Client: {clientId} does not exist. Message = '{e.Message}'");

                return ValidationState.UnsupportedClient;
            }

            _logger.LogTrace("Checking for empty basket");
            
            var validatePortfolioHasStockResponse = _globalValidationService.ValidatePortfolioHasStock(portfolio);

            if (validatePortfolioHasStockResponse != ValidationState.Success)
            {
                _logger.LogWarning("Basket cannot be empty");
                
                return validatePortfolioHasStockResponse;
            }
            
            decimal weightTotal = 0;
            decimal notionalAmountTotal = 0;

            foreach (var stock in portfolio.GetStocks())
            {
                _logger.LogTrace($"Validating stock with global rules, OrderId ='{stock.OrderId}'");
                
                var globalValidateStockResponse = _globalValidationService.ValidateStock(stock);

                if (globalValidateStockResponse != ValidationState.Success)
                {
                    _logger.LogWarning($"Failed validating stock with global rules, OrderId ='{stock.OrderId}");
                    
                    return globalValidateStockResponse;
                }

                _logger.LogTrace("Stock with OrderId has passed global validation");

                var clientValidateStockResponse = clientValidation.ValidateStock(stock);

                if (clientValidateStockResponse != ValidationState.Success)
                {
                    _logger.LogWarning($"Failed validating stock with {clientValidation.GetType().Name} rules, OrderId ='{stock.OrderId}");
                    
                    return clientValidateStockResponse;
                }

                weightTotal += stock.Weight;
                
                notionalAmountTotal += stock.NotionalAmount;
                
            }
            
            _logger.LogTrace("Stock validation passed... validating portfolio weight");

            var totalPortfolioWeightResponse = _globalValidationService.ValidateTotalPortfolioWeight(weightTotal);

            if (totalPortfolioWeightResponse != ValidationState.Success)
            {
                _logger.LogWarning("Total weight does not meet weight total value: 1");
                return ValidationState.InvalidWeightState;
            }

            var totalPortfolioNotionalAmountResponse = clientValidation.ValidateTotalPortfolioNotionalAmount(notionalAmountTotal);

            if (totalPortfolioNotionalAmountResponse != ValidationState.Success)
            {
                _logger.LogWarning(
                    $"Failed validating stock with {clientValidation.GetType().Name} total notional amount rules");
                
                return totalPortfolioNotionalAmountResponse;
            }
            
            _logger.LogTrace("Portfolio validation success");
            
            return ValidationState.Success;
        }

    }
}
