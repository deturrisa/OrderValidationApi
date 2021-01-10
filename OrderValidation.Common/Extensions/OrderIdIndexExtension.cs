using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common.Extensions
{
    public static class OrderIdIndexExtension
    {
        public static int GetIndexFromOrderId(this string orderId)
        {
            if (!int.TryParse(orderId.Split('-')[2], out var index))
            {
                throw new InvalidOperationException($"Failed to retrieve index from OrderId= '{orderId}");
            }

            return index;
        }

        public static string GetDateFromOrderId(this string orderId)
        {
            if (!DateTime.TryParse(orderId.Split('-')[1], out var date))
            {
                throw new InvalidOperationException($"Failed to retrieve Date from OrderId= '{orderId}");
            }
            
            return $"{date.Day:00}/{date.Month:00}/{date.Year}";
        }
    }
}
