using OrderValidation.Common.NotionalValidation;

namespace OrderValidation.Client.Clients.Interfaces
{
    public interface IClientBValidationService : IClientValidation, IStockNotionalAmountValidation, IPortfolioNotionalAmountValidation
    {
    }
}
