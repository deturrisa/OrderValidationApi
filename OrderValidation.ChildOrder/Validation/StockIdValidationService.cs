using System;
using Microsoft.Extensions.Logging;
using OrderValidation.Common;
using OrderValidation.Common.Extensions;
using OrderValidation.Common.DateTimeWrapper;

namespace OrderValidation.ChildOrder.Validation
{
    public class StockIdValidationService: IStockIdValidationService
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly ILogger<StockIdValidationService> _logger;
        
        public StockIdValidationService(IDateTimeWrapper dateTimeWrapper, ILogger<StockIdValidationService> logger)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _logger = logger;
        }

        public ValidationState ValidateOrderId(string stockId, int previousIndex)
        {
            try
            {
                var currentStockDate = stockId.GetDateFromOrderId();
                
                var currentIndex = stockId.GetIndexFromOrderId();
                
                _logger.LogTrace("Retrieving previous OrderId Index");

                
                if (currentIndex <= previousIndex)
                {
                    _logger.LogWarning($"Current OrderId index: {currentIndex} can not be less than or equal previous OrderId index: {previousIndex}");
                    return ValidationState.InvalidOrderId;
                }
                
                _logger.LogTrace("Current OrderId index is valid");

                var today = $"{_dateTimeWrapper.Now():dd/MM/yyyy}";

                if (currentStockDate != today)
                {
                    _logger.LogWarning($"Stock date: {currentStockDate} is not equal to today's date: {today}");
                    return ValidationState.InvalidOrderId;
                }

                _logger.LogTrace("Storing index for next validation");

                return ValidationState.Success;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning($"Invalid OrderId provided Exception = {e.Message}");
                return ValidationState.InvalidOrderId;
            }

        }

    }
}
