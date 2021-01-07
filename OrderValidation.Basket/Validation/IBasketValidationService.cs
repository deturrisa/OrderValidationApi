using System;
using System.Collections.Generic;
using System.Text;
using OrderValidation.Common;

namespace OrderValidation.Basket.Validation
{
    public interface IBasketValidationService
    {
       ValidationState ValidateBasket(Basket basket);
    }
}
