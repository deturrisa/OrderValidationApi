using OrderValidation.Common;

namespace OrderValidation.ChildOrder.Validation
{
    public interface IStockIdValidationService
    {
        ValidationState ValidateOrderId(string stockId);
    }
}