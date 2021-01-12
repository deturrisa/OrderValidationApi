namespace OrderValidation.Common.NotionalValidation
{
    public interface IStockNotionalAmountValidation
    {
        ValidationState ValidateStockNotionalAmount(decimal notionalAmount);
    }
}
