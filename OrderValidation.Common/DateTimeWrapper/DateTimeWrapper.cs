using System;

namespace OrderValidation.Common.DateTimeWrapper
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime Now()=>  DateTime.Now;
    }
}
