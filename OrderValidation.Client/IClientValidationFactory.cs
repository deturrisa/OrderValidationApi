using OrderValidation.Client.Clients;

namespace OrderValidation.Client
{
    public interface IClientValidationFactory
    {
        IClientValidation GetClientValidation(string clientId);
    }
}