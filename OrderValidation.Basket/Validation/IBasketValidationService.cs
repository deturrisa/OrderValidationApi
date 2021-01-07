using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Basket.Validation
{
    public interface IBasketValidationService
    {
       bool ValidateBasket(IBasket basket);
    }
}
