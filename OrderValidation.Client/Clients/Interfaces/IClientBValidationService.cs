using OrderValidation.Common.Destination;
using OrderValidation.Common.NotionalValidation;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientBValidationService : IClientValidation, IStockNotionalAmountValidation,IDestinationValidation
    {
    }
}
