using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;
using OrderValidation.Common.DateTimeWrapper;

namespace OrderValidation.ChildOrder
{
    public class StockIdGenerator : IStockIdGenerator
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly ILogger<IStockIdGenerator> _logger;

        public StockIdGenerator(IDateTimeWrapper dateTimeWrapper, ILogger<IStockIdGenerator> logger)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _logger = logger;
        }

        public string GenerateDateIndexId(int index)
        {
            var id = $"QF-{_dateTimeWrapper.Now()}-{index}";
            _logger.LogTrace($"Generated Id {id}");
            return id;
        }
    }
}
