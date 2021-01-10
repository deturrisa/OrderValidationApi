﻿using OrderValidation.Common;
using OrderValidation.Common.NotionalValidation;
using OrderValidation.Common.Type;
using OrderValidation.Currency.Validation;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientValidation : ICurrencyValidationService
    {
        ValidationState ValidateStock(Stock stock);
        ValidationState ValidatePortfolioNotionalAmount(decimal amount);
    }
}