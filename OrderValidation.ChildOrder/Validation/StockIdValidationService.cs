using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheKey = "OrderId";
        public StockIdValidationService(IDateTimeWrapper dateTimeWrapper, ILogger<StockIdValidationService> logger, IMemoryCache memoryCache)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public ValidationState ValidateOrderId(string stockId)
        {
            try
            {
                var currentStockDate = stockId.GetDateFromOrderId();
                
                var currentIndex = stockId.GetIndexFromOrderId();
                
                _logger.LogTrace("Retrieving previous OrderId Index");

                var previousIndex = GetPreviousIndex(currentIndex);
                
                if (currentIndex <= previousIndex)
                {
                    _logger.LogWarning($"Current OrderId index: {currentIndex} can not be less than or equal previous OrderId index: {previousIndex}");
                    return ValidationState.InvalidOrderId;
                }

                _logger.LogTrace("Current OrderId index is valid");

                SetCacheWithExpiration(currentIndex);

                var today = $"{_dateTimeWrapper.Now():dd/MM/yyyy}";

                if (currentStockDate != today)
                {
                    _logger.LogWarning($"Stock date: {currentStockDate} is not equal to today's date: {today}");
                    return ValidationState.InvalidOrderId;
                }

                return ValidationState.Success;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning($"Invalid OrderId provided Exception = {e.Message}");
                return ValidationState.InvalidOrderId;
            }

        }

        private int GetPreviousIndex(int currentIndex)
        {
            if (!_memoryCache.TryGetValue(_cacheKey, out int previousIndex))
            {
                _logger.LogTrace($"Cache is empty. Setting cache with index: {currentIndex}");

                
                SetCacheWithExpiration(currentIndex);
            }

            return previousIndex;
        }

        private void SetCacheWithExpiration(int index)
        {
            //ToDo: change this to set expiration date +1 day
            _memoryCache.Set(_cacheKey, index,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5)));
        }
    }
}
