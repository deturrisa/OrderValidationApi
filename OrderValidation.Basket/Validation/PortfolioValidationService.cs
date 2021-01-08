using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using OrderValidation.Common;

namespace OrderValidation.Basket.Validation
{
    public class PortfolioValidationService : IPortfolioValidationService
    {
        private readonly ILogger<PortfolioValidationService> _logger;
        public PortfolioValidationService(ILogger<PortfolioValidationService> logger)
        {
            _logger = logger;
        }
        
        public ValidationState ValidateBasket(Portfolio portfolio)
        {
            _logger.LogTrace("Attempting to validate portfolio");
            
            var stocks = portfolio.GetStocks();

            if (stocks.Count == 0)
            {
                _logger.LogWarning("Portfolio cannot be empty");
                return ValidationState.EmptyPortfolio;
            }
            
            decimal total = 0;

            foreach (var stock in stocks)
            {
                if (stock.Weight < 0)
                {
                    _logger.LogWarning("Weight properties cannot be < 0");
                    return ValidationState.NegativeStockWeight;
                }

                total += stock.Weight;
            }

            if (total != 1)
            {
                _logger.LogWarning("Total weight of orders must be equal to 1");
                return ValidationState.InvalidWeightState;
            }

            _logger.LogTrace("Portfolio validation success");
            return ValidationState.Success;
        }

    }
}
