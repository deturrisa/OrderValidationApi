using OrderValidation.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OrderValidation.Basket
{
    public class Basket : IEnumerable<ChildOrder>
    {
        private List<ChildOrder> _childOrders = new List<ChildOrder>();
        public int Count => _childOrders.Count;

        public decimal SumOrderWeight() => _childOrders.Sum(x => x.Weight);

        public List<ChildOrder> GetChildOrders() => _childOrders;

        public IEnumerator<ChildOrder> GetEnumerator()
        {
            return _childOrders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ChildOrder childOrder)
        {
            _childOrders.Add(childOrder);
        }
        
        public void Add(List<ChildOrder> childOrders)
        {
            foreach (var childOrder in childOrders)
            {
                Add(childOrder);
            }
        }

        public ChildOrder this[int index] => _childOrders[index];
    }
}
