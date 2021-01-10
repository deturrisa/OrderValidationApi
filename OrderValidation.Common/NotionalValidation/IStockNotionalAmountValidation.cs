using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common.NotionalValidation
{
    public interface IStockNotionalAmountValidation
    {
        ValidationState ValidateStockNotionalAmount(decimal notionalAmount);
    }
}
