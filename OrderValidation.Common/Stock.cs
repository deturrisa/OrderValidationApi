using System;
using System.ComponentModel.DataAnnotations;

namespace OrderValidation.Common
{
    public class Stock
    {
        private decimal _weight;
        private decimal _notionalAmount;

        [Range(double.Epsilon, 1.0)]
        public decimal Weight
        {
            get => Math.Round(_weight, 2);
            set => _weight = value;
        }

        public decimal NotionalAmount
        {
            get => Math.Round(_notionalAmount, 2);
            set => _notionalAmount = value;
        }
        public string OrderId { get; set; }
        public string Currency { get; set; }
    }
}
