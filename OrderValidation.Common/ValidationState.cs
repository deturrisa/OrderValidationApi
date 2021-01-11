using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common
{
    public enum ValidationState
    {
        None = 0,
        Success = 1,
        InvalidCurrencyFormat = 2,
        NegativeStockWeight = 3,
        InvalidWeightState = 4,
        EmptyPortfolio = 5,
        UnsupportedCurrency = 6,
        UnsupportedClient = 7,
        InvalidOrderId = 8,
        UnsupportedType = 9,
        UnsupportedDestination=10,
        MinimumStockNotionalAmountNotMet=11,
        MinimumPortfolioNotionalAmountNotMet=12,
        MaximumPortfolioNotionalAmountNotMet=14,
        NegativeNotionalWeight=15,
        InvalidCurrencySymbol=16
    }
}
