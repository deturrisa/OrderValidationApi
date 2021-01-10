using OrderValidation.Common.Destination;
using OrderValidation.Common.NotionalValidation;
using OrderValidation.Common.Type;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientAValidationService : IClientValidation, IStockNotionalAmountValidation,IDestinationValidation,ITypeValidation
    {
    }
}
