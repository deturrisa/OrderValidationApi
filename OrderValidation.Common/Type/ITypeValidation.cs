namespace OrderValidation.Common.Type
{
    public interface ITypeValidation
    {
        ValidationState ValidateType(OrderType orderType);
    }
}
