using System;
using OrderValidation.Client.Clients;
using OrderValidation.Client.Clients.Interfaces;

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
