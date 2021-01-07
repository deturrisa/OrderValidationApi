using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common
{
    public enum ValidationState
    {
        None = 0,
        Success = 1,
        UnsupportedCurrency = 2,
        NegativeChildOrderWeight = 3,
        InvalidWeightState = 4,
        EmptyBasket = 5,
    }
}
