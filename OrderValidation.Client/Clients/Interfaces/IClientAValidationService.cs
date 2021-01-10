using OrderValidation.Common.NotionalValidation;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientAValidationService : IClientValidation, IStockNotionalAmountValidation
    {
    }
}
