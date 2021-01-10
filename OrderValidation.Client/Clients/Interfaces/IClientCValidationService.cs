using OrderValidation.Common;
using OrderValidation.Common.NotionalValidation;
using OrderValidation.Common.Type;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientCValidationService : IClientValidation
    {
        ValidationState ValidateDestination(string destination, OrderType orderType);
    }
}
