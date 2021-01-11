using System;
using System.Collections.Generic;
using System.Text;
using OrderValidation.Common.Requests;

namespace OrderValidation.Service
{
    public interface IValidationService
    {
        OrdersValidationResponse ValidateOrders(string clientId, OrdersRequest request);
    }
}
