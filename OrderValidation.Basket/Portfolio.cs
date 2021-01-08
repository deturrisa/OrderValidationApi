using OrderValidation.ChildOrder;
using OrderValidation.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OrderValidation.Basket
{
    public class Portfolio : IEnumerable<Stock>
    {
        private List<Stock> _stocks = new List<Stock>();
        public int Count => _stocks.Count;

        public decimal SumOrderWeight() => _stocks.Sum(x => x.Weight);

        public List<Stock> GetStocks() => _stocks;

        public IEnumerator<Stock> GetEnumerator()
        {
            return _stocks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Stock stock)
        {
            _stocks.Add(stock);
        }
        
        public void Add(List<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                Add(stock);
            }
        }

        public Stock this[int index] => _stocks[index];
    }
}
