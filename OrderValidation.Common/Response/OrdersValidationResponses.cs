namespace OrderValidation.Common
{
    public static class OrdersValidationResponses
    {
        public static OrdersValidationResponse ClientIdNotFound { get; } =
            new OrdersValidationResponse(10001, "The client id in the request cannot be found");
        public static OrdersValidationResponse InvalidOrderId { get; } =
            new OrdersValidationResponse(10002, "An invalid OrderId was supplied. No increment in OrderId or does not meet today's date");
        public static OrdersValidationResponse NegativeStockWeight { get; } =
            new OrdersValidationResponse(10004, "Stock weight in order cannot be negative");
        public static OrdersValidationResponse NegativeNotionalAmount { get; } =
            new OrdersValidationResponse(10005, "Notional Amount in order cannot be negative");
        public static OrdersValidationResponse InvalidPortfolioWeight { get; } =
            new OrdersValidationResponse(10006, "Portfolio basket cannot be empty");
        public static OrdersValidationResponse EmptyPortfolio { get; } =
            new OrdersValidationResponse(10007, "Portfolio basket cannot be empty");
        public static OrdersValidationResponse InvalidSymbol { get; } =
            new OrdersValidationResponse(10008, "Currency symbol is invalid");
        public static OrdersValidationResponse NullRequest { get; } =
            new OrdersValidationResponse(10009, "Request cannot be null");
        public static OrdersValidationResponse UnsupportedType { get; } =
            new OrdersValidationResponse(20001, "The type is not supported for this client");
        public static OrdersValidationResponse UnsupportedCurrency { get; } =
            new OrdersValidationResponse(20002, "The currency is not supported for this client");
        public static OrdersValidationResponse UnsupportedDestination { get; } =
            new OrdersValidationResponse(20003, "The destination is not supported for this client");
        public static OrdersValidationResponse MinimumStockNotionalAmountNotMet { get; } =
            new OrdersValidationResponse(20004, "The minimum stock notional amount for this client has not been met");
        public static OrdersValidationResponse MinimumPortfolioNotionalAmountNotMet { get; } =
            new OrdersValidationResponse(20005, "The minimum portfolio notional amount for this client has not been met");
        public static OrdersValidationResponse MaximumStockNotionalAmountNotMet { get; } =
            new OrdersValidationResponse(20006, "The maximum stock notional amount for this client has been exceeded");
        public static OrdersValidationResponse InvalidCurrencyFormat { get; } =
            new OrdersValidationResponse(20006, "The currency does not meet the ISO4217 format");
        public static OrdersValidationResponse Success { get; } =
            new OrdersValidationResponse(30000, "The orders were successfully validated",true);
        public static OrdersValidationResponse UnknownError { get; } =
            new OrdersValidationResponse(30000, "An Unknown Error has occurred");

    }
}

public class OrdersValidationResponse
{
    public int Code { get; }
    public string Message { get; }
    public bool Success { get; }

    public OrdersValidationResponse(int code, string message, bool success=false)
    {
        Code = code;
        Message = message;
        Success = success;
    }
}
