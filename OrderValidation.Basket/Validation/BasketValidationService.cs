using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using OrderValidation.Common;

namespace OrderValidation.Basket.Validation
{
    public class BasketValidationService : IBasketValidationService
    {
        private readonly ILogger<BasketValidationService> _logger;
        public BasketValidationService(ILogger<BasketValidationService> logger)
        {
            _logger = logger;
        }
        
        public ValidationState ValidateBasket(Basket basket)
        {
            _logger.LogTrace("Attempting to validate basket");
            
            var childOrders = basket.GetChildOrders();

            if (childOrders.Count == 0)
            {
                _logger.LogWarning("Basket cannot be empty");
                return ValidationState.EmptyBasket;
            }
            
            decimal total = 0;

            foreach (var childOrder in childOrders)
            {
                if (childOrder.Weight < 0)
                {
                    _logger.LogWarning("Weight properties cannot be < 0");
                    return ValidationState.NegativeChildOrderWeight;
                }

                total += childOrder.Weight;
            }

            if (total != 1)
            {
                _logger.LogWarning("Total weight of orders must be equal to 1");
                return ValidationState.InvalidWeightState;
            }

            _logger.LogTrace("Basket validation success");
            return ValidationState.Success;
        }

    }
}
