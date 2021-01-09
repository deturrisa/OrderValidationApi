using OrderValidation.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Client.Global
{
    public class GlobalValidationService : IGlobalValidationService
    {
        public ValidationState ValidatePortfolioHasStock(Portfolio portfolios)
        {
            throw new NotImplementedException();
        }

        public ValidationState ValidateStock(Stock stock)
        {
            throw new NotImplementedException();
        }

        public ValidationState ValidateTotalPortfolioWeight(decimal totalWeight)
        {
            throw new NotImplementedException();
        }
    }
}
