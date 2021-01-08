using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OrderValidation.Common.DateTimeWrapper;

namespace OrderValidation.ChildOrder
{
    public class StockIdGenerator : IStockIdGenerator
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;

        public StockIdGenerator(IDateTimeWrapper dateTimeWrapper)
        {
            _dateTimeWrapper = dateTimeWrapper;
        }

        public string GenerateDateIndexId(int index) => $"QF-{_dateTimeWrapper.Now()}-{index}";
    }
}
