using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using OrderValidation.Client.Clients.Interfaces;
using OrderValidation.Common;
using OrderValidation.Common.Type;

namespace OrderValidation.Client.Clients
{
    public class ClientAValidationService : IClientAValidationService
    {
        private readonly ILogger<ClientAValidationService> _logger;
        
        private const string SupportedDestination = "DestinationA";
        
        private const OrderType SupportedOrderType = OrderType.Market;
        
        private const string SupportedCurrency = "HKD";

        private const decimal MinimumChildNotionalAmount = 100m;
        
        public ClientAValidationService(ILogger<ClientAValidationService> logger)
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

            if (ValidateType(stock.OrderType) != ValidationState.Success)
            {
                _logger.LogWarning($"Currency: {stock.Currency} is not supported with this client: {stock.ClientId}");
                return ValidateType(stock.OrderType);
            }

            if (ValidateDestination(stock.Destination) != ValidationState.Success)
            {
                _logger.LogWarning($"Destination: {stock.Destination} is not supported with this client: {stock.ClientId}");
                return ValidateDestination(stock.Destination);
            }

            if (ValidateStockNotionalAmount(stock.NotionalAmount) != ValidationState.Success)
            {
                _logger.LogWarning($"NotionalAmount: {stock.NotionalAmount} does not meet Client Minimum Notional Amount. ClientId: {stock.ClientId}");
                return ValidateStockNotionalAmount(stock.NotionalAmount);
            }
            
            return ValidationState.Success;
        }
        public ValidationState ValidateType(OrderType orderType)
        {
            return orderType != SupportedOrderType ? ValidationState.UnsupportedType : ValidationState.Success;
        }

        public ValidationState ValidatePortfolioNotionalAmount(decimal notionalAmountTotal)
        {
            throw new NotImplementedException(); //TODO: delete when moved from interface
        }

        public ValidationState ValidateDestination(string destination)
        {
            return destination != SupportedDestination ? ValidationState.UnsupportedDestination : ValidationState.Success;
        }

        public ValidationState ValidateCurrency(string currency)
        {
            return currency != SupportedCurrency ? ValidationState.UnsupportedCurrency : ValidationState.Success;
        }

        public ValidationState ValidateStockNotionalAmount(decimal notionalAmount)
        {
            return notionalAmount < MinimumChildNotionalAmount ? ValidationState.MinimumStockNotionalAmountNotMet : ValidationState.Success;
        }
    }
}
