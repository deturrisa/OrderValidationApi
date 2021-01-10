using System;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.Client.Clients;
using OrderValidation.Common;
using Xunit;
using AutoFixture.Xunit2;
using OrderValidation.Common.Type;

namespace OrderValidation.Client.Tests.Clients
{
    public class ClientAValidationServiceTests
    {
        [Fact]
        public void ValidateType_ReturnsUnsupportedTypeState_WhenTypeUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();
            
            var sut = new ClientAValidationService(mockLogger.Object);
            var orderType = It.Is<OrderType>(x => x != OrderType.Market);
            
            //Act
            var result = sut.ValidateType(orderType);
            
            //Assert
            Assert.Equal(ValidationState.UnsupportedType,result);
        }
        
        [Fact]
        public void ValidateType_ReturnsSuccessState_WhenTypeSupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();
            
            var sut = new ClientAValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateType(OrderType.Market);
            
            //Assert
            Assert.Equal(ValidationState.Success,result);
        }
        
        [Fact]
        public void ValidateCurrency_ReturnsUnsupportedCurrencyState_WhenCurrencyUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();

            var sut = new ClientAValidationService(mockLogger.Object);
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
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();

            var sut = new ClientAValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateCurrency("HKD");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Fact]
        public void ValidateDestination_ReturnsUnsupportedDestinationState_WhenDestinationUnsupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();

            var sut = new ClientAValidationService(mockLogger.Object);
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
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();

            var sut = new ClientAValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateDestination("DestinationA");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Theory, AutoData]
        public void ValidateStockNotionalAmount_ReturnsMinimumStockNotionalAmountNotMetState_WhenMinimumStockNotionalAmountNotMet(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();

            var sut = new ClientAValidationService(mockLogger.Object);

            var notionalAmount = 100 - amount;
            
            //Act
            var result = sut.ValidateStockNotionalAmount(notionalAmount);

            //Assert
            Assert.Equal(ValidationState.MinimumStockNotionalAmountNotMet, result);
        }

        [Theory, AutoData]
        public void ValidateStock_ReturnsSuccess_WhenMinimumStockNotionalAmountMet(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();

            var sut = new ClientAValidationService(mockLogger.Object);

            var notionalAmount = 100 + amount;

            //Act
            var result = sut.ValidateStockNotionalAmount(notionalAmount);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Theory,AutoData]
        public void ValidateStock_ReturnsSuccessStateWhenAll_ValidationReturnsSuccessState(decimal amount)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ClientAValidationService>>();

            var sut = new ClientAValidationService(mockLogger.Object);

            var notionalAmount = 100 + amount;

            var stock = new Stock()
            {
                Currency = "HKD", 
                Destination = "DestinationA", 
                NotionalAmount = 100 + notionalAmount,
                OrderType = OrderType.Market
            };

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }
        
    }

}
