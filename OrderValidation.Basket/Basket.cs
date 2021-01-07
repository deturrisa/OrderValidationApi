using OrderValidation.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OrderValidation.Basket
{
    public class Basket : IBasket
    {
        private readonly List<ChildOrder> _childOrders;
        public Basket(List<ChildOrder> childOrders)
        {
            _childOrders = childOrders;
        }
        public int CountChildOrders() => _childOrders.Count;

        public decimal SumOrderWeight() => _childOrders.Sum(x => x.Weight);

        public List<ChildOrder> GetChildOrders() => _childOrders;

        //TODO See if can implement ICollection or IEnumerable
    }
}
