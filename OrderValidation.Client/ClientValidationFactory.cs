using System;
using OrderValidation.Client.Clients;

namespace OrderValidation.Client
{
    public class ClientValidationFactory : IClientValidationFactory
    {
        public IClientValidation GetClientValidation(string clientId)
        {
            throw new ArgumentException();
        }
    }
}
