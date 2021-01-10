using System;
using System.Collections.Generic;
using OrderValidation.Client.Clients.Interfaces;

namespace OrderValidation.Client
{
    public class ClientValidationFactory : IClientValidationFactory
    {
        private readonly IDictionary<string, Func<IClientValidation>> _clientValidationDictionary;

        public ClientValidationFactory(Lazy<IClientAValidationService> clientAValidationService,
            Lazy<IClientBValidationService> clientBValidationService,
            Lazy<IClientCValidationService> clientCValidationService)
        {
            _clientValidationDictionary = new Dictionary<string, Func<IClientValidation>>
            {
                {"A",() => clientAValidationService.Value },
                {"B",() => clientBValidationService.Value },
                {"C",() => clientCValidationService.Value }
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
