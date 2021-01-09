using System;
using System.Collections.Generic;
using System.Text;
using OrderValidation.Common;

namespace OrderValidation.Basket.Validation
{
    public interface IPortfolioValidationService
    {
       ValidationState ValidatePortfolio(Portfolio portfolio, string clientId);
    }
}
