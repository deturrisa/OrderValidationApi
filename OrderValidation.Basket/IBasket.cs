using System;
using System.Collections.Generic;
using System.Text;
using OrderValidation.Common;

namespace OrderValidation.Basket
{
    public interface IBasket
    {
        int CountChildOrders();
        decimal SumOrderWeight();
        List<ChildOrder> GetChildOrders();
    }
}
