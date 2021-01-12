using System;

namespace OrderValidation.Common.Extensions
{
    public static class OrderFormatExtension
    {
        public static int GetIndexFromOrderId(this string orderId)
        {
            string orderIdIndex;
            try
            {
                orderIdIndex = orderId.Split('-')[2];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException($"Failed to retrieve index from OrderId= '{orderId}");
            }
            if (!int.TryParse(orderIdIndex, out var index))
            {
                throw new InvalidOperationException($"Failed to retrieve index from OrderId= '{orderId}");
            }

            return index;
        }

        public static string GetDateFromOrderId(this string orderId)
        {
            string orderIdDate;
            
            try
            {
                orderIdDate = orderId.Split('-')[1];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException($"Failed to retrieve Date from OrderId= '{orderId}");
            }
            if (!DateTime.TryParse(orderIdDate, out var date))
            {
                throw new InvalidOperationException($"Failed to retrieve Date from OrderId= '{orderId}");
            }
            
            return $"{date.Day:00}/{date.Month:00}/{date.Year}";
        }
    }
}
