using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.Client.Clients;
using OrderValidation.Common;
using OrderValidation.Common.Type;
using Xunit;

namespace OrderValidation.Client.Tests.Clients
{
    public class ClientCValidationServiceTests
    {
        [Fact]
        public void ValidateDestination_ReturnsUnsupportedTypeState_WhenTypeUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientCValidationService>>();

            var sut = new ClientCValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateDestination(It.IsAny<string>(),OrderType.None);

            //Assert
            Assert.Equal(ValidationState.UnsupportedType, result);
        }

        [Theory]
        [InlineAutoData(OrderType.Market,"DestinationA")]
        [InlineAutoData(OrderType.Limit,"DestinationB")]
        public void ValidateDestination_ReturnsSuccessState_WhenTypeSupported_WithDestination(OrderType orderType,string destination)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientCValidationService>>();

            var sut = new ClientCValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateDestination(destination, orderType);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Fact]
        public void ValidateCurrency_ReturnsUnsupportedCurrencyState_WhenCurrencyUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientCValidationService>>();

            var sut = new ClientCValidationService(mockLogger.Object);
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
            var mockLogger = new Mock<ILogger<ClientCValidationService>>();

            var sut = new ClientCValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateCurrency("USD");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Theory, AutoData]
        public void ValidatePortfolioNotionalAmount_ReturnsMaximumPortfolioNotionalAmountNotMetState_WhenMaximumPortfolioNotionalAmountExceeded(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientCValidationService>>();

            var sut = new ClientCValidationService(mockLogger.Object);

            var notionalAmount = amount + 100000;

            //Act
            var result = sut.ValidatePortfolioNotionalAmount(notionalAmount);

            //Assert
            Assert.Equal(ValidationState.MaximumPortfolioNotionalAmountNotMet, result);
        }

        [Theory, AutoData]
        public void ValidatePortfolioNotionalAmount_ReturnsSuccessState_WhenMaximumStockNotionalAmountNotMet(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientCValidationService>>();

            var sut = new ClientCValidationService(mockLogger.Object);

            var notionalAmount = 10000 - amount ;

            //Act
            var result = sut.ValidatePortfolioNotionalAmount(notionalAmount);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Theory]
        [InlineAutoData(OrderType.Market, "DestinationA")]
        [InlineAutoData(OrderType.Limit, "DestinationB")]
        public void ValidateStock_ReturnsSuccessStateWhenAll_ValidationReturnsSuccessState(OrderType orderType, string destination, decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientCValidationService>>();

            var sut = new ClientCValidationService(mockLogger.Object);

            var stock = new Stock()
            {
                Currency = "USD",
                Destination = destination,
                NotionalAmount = amount,
                OrderType = orderType
            };

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

    }
}
