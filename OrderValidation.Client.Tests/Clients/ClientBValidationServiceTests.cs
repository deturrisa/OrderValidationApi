using System;
using OrderValidation.Client.Clients;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.Common;
using OrderValidation.Common.Type;
using Xunit;

namespace OrderValidation.Client.Tests.Clients
{
    public class ClientBValidationServiceTests
    {
        [Fact]
        public void ValidateType_ReturnsUnsupportedTypeState_WhenTypeUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);
            var orderType = It.Is<OrderType>(x => x != OrderType.Market);

            //Act
            var result = sut.ValidateType(orderType);

            //Assert
            Assert.Equal(ValidationState.UnsupportedType, result);
        }

        [Fact]
        public void ValidateType_ReturnsSuccessState_WhenTypeSupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateType(OrderType.Limit);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Fact]
        public void ValidateCurrency_ReturnsUnsupportedCurrencyState_WhenCurrencyUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);
            var currency = Guid.NewGuid().ToString().Substring(0, 2);

            //Act
            var result = sut.ValidateCurrency(currency);

            //Assert
            Assert.Equal(ValidationState.UnsupportedCurrency, result);
        }

        [Fact]
        public void ValidateCurrency_ReturnsSuccessState_WhenCurrencySupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateCurrency("USD");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Fact]
        public void ValidateDestination_ReturnsUnsupportedDestinationState_WhenDestinationUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);
            var destination = Guid.NewGuid().ToString();

            //Act
            var result = sut.ValidateDestination(destination);

            //Assert
            Assert.Equal(ValidationState.UnsupportedDestination, result);
        }

        [Fact]
        public void ValidateDestination_SupportedDestinationState_WhenDestinationSupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateDestination("DestinationB");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Theory, AutoData]
        public void ValidateStockNotionalAmount_ReturnsMinimumStockNotionalAmountNotMetState_WhenMinimumStockNotionalAmountNotMet(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);

            var notionalAmount = 1000 - amount;

            //Act
            var result = sut.ValidateStockNotionalAmount(notionalAmount);

            //Assert
            Assert.Equal(ValidationState.MinimumStockNotionalAmountNotMet, result);
        }

        [Theory, AutoData]
        public void ValidateStock_ReturnsSuccess_WhenMinimumStockNotionalAmountMet(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);

            var notionalAmount = 1000 + amount;

            //Act
            var result = sut.ValidateStockNotionalAmount(notionalAmount);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Theory, AutoData]
        public void ValidateStock_ReturnsSuccessStateWhenAll_ValidationReturnsSuccessState(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientBValidationService>>();

            var sut = new ClientBValidationService(mockLogger.Object);

            var notionalAmount = 100 + amount;

            var stock = new Stock()
            {
                Currency = "USD",
                Destination = "DestinationB",
                NotionalAmount = 1000 + notionalAmount,
                OrderType = OrderType.Limit
            };

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

    }
}
