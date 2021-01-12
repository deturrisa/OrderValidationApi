using OrderValidation.Client.Clients.Interfaces;

namespace OrderValidation.Client
{
    public interface IClientValidationFactory
    {
        IClientValidation GetClientValidation(string clientId);
    }
}