using System;
using System.Collections.Generic;
using OrderValidation.Client.Clients.Interfaces;

namespace OrderValidation.Client
{
    public class ClientValidationFactory : IClientValidationFactory
    {
        private readonly IDictionary<string, Func<IClientValidation>> _clientValidationDictionary;

        public ClientValidationFactory(IClientAValidationService clientAValidationService,
            IClientBValidationService clientBValidationService,
            IClientCValidationService clientCValidationService)
        {
            _clientValidationDictionary = new Dictionary<string, Func<IClientValidation>>
            {
                {"A",() => clientAValidationService },
                {"B",() => clientBValidationService },
                {"C",() => clientCValidationService }
            };
        }
        public IClientValidation GetClientValidation(string clientId)
        {
            if (!_clientValidationDictionary.TryGetValue(clientId, out var getProvider))
            {
                throw new ArgumentOutOfRangeException($"Invalid ClientId '{clientId}'");
            }

            return getProvider();
        }
    }
}
