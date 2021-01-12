using OrderValidation.Common.Requests;

namespace OrderValidation.Service
{
    public interface IValidationService
    {
        OrdersValidationResponse ValidateOrders(string clientId, OrdersRequest request);
    }
}
