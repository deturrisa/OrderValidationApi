using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OrderValidation.Client.Clients.Interfaces;
using OrderValidation.Common;
using OrderValidation.Common.Type;


namespace OrderValidation.Client.Clients
{
    public class ClientCValidationService : IClientCValidationService
    {
        private readonly ILogger<ClientCValidationService> _logger;

        private Dictionary<OrderType, string> TypeWithCorrespondingDestination => new Dictionary<OrderType, string>()
        {
            { OrderType.Market, "DestinationA"},
            { OrderType.Limit, "DestinationB"},
        };
        
        private const string SupportedCurrency = "USD";

        private const decimal MaximumBasketNotionalAmount = 100000;
        
        public ClientCValidationService(ILogger<ClientCValidationService> logger)
        {
            _logger = logger;
        }

        public ValidationState ValidateStock(Stock stock)
        {
            if (ValidateCurrency(stock.Currency) != ValidationState.Success)
            {
                _logger.LogWarning($"Currency: {stock.Currency} is not supported with client: {stock.ClientId}");
                return ValidateCurrency(stock.Currency);
            }

            if (ValidateDestination(stock.Destination,stock.OrderType) != ValidationState.Success)
            {
                _logger.LogWarning($"Destination: {stock.Destination} with OrderType: {stock.OrderType} is not supported with this client: {stock.ClientId}");
                return ValidateDestination(stock.Destination, stock.OrderType);
            }
            
            return ValidationState.Success;
        }
        
        public ValidationState ValidatePortfolioNotionalAmount(decimal notionalAmountTotal)
        {
            return notionalAmountTotal > MaximumBasketNotionalAmount ? ValidationState.MaximumPortfolioNotionalAmountNotMet : ValidationState.Success;
        }

        public ValidationState ValidateDestination(string destination, OrderType orderType)
        {
            if (!TypeWithCorrespondingDestination.TryGetValue(orderType, out var destinationValue))
            {
                return ValidationState.UnsupportedType;
            }

            return destination != destinationValue ? ValidationState.UnsupportedDestination : ValidationState.Success;
        }

        public ValidationState ValidateCurrency(string currency)
        {
            return currency != SupportedCurrency ? ValidationState.UnsupportedCurrency : ValidationState.Success;
        }

    }
}
