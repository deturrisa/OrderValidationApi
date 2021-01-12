namespace OrderValidation.Common.Destination
{
    public interface IDestinationValidation
    {
        ValidationState ValidateDestination(string destination);
    }
}
