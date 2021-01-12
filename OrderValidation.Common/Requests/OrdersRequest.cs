using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common.Requests
{
    public class OrdersRequest
    {
        public List<Stock> Stocks { get; set; }
        public string Symbol { get; set; }
    }
}
