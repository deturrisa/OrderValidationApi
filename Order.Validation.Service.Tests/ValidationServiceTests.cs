using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Moq;
using OrderValidation.Basket.Validation;
using OrderValidation.Common;
using OrderValidation.Common.Requests;
using OrderValidation.Service;
using Xunit;

namespace Order.Validation.Service.Tests
{
    public class ValidationServiceTests
    {
        [Theory]
        [InlineAutoData(ValidationState.InvalidCurrencyFormat)]
        [InlineAutoData(ValidationState.NegativeStockWeight)]
        [InlineAutoData(ValidationState.InvalidWeightState)]
        [InlineAutoData(ValidationState.EmptyPortfolio)]
        [InlineAutoData(ValidationState.UnsupportedCurrency)]
        [InlineAutoData(ValidationState.UnsupportedClient)]
        [InlineAutoData(ValidationState.InvalidOrderId)]
        [InlineAutoData(ValidationState.UnsupportedType)]
        [InlineAutoData(ValidationState.UnsupportedDestination)]
        [InlineAutoData(ValidationState.MinimumStockNotionalAmountNotMet)]
        [InlineAutoData(ValidationState.MinimumPortfolioNotionalAmountNotMet)]
        [InlineAutoData(ValidationState.MaximumPortfolioNotionalAmountNotMet)]
        [InlineAutoData(ValidationState.NegativeNotionalWeight)]
        [InlineAutoData(ValidationState.InvalidCurrencySymbol)]
        public void ValidateRequest_ReturnsSuccessFalseInResponse_WhenPortfolioValidationServiceReturnsUnsuccessfulValidationState(ValidationState validationState,List<Stock> stocks)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ValidationService>>();
            
            var mockPortfolioValidationService = new Mock<IPortfolioValidationService>();
            
            var validationService = new ValidationService(mockLogger.Object,mockPortfolioValidationService.Object);
            
            var ordersRequest = new OrdersRequest() {Stocks = stocks};
            var portfolio = new Portfolio {stocks};
            
            mockPortfolioValidationService.Setup(x => x.ValidatePortfolio(portfolio, It.IsAny<string>()))
                .Returns(validationState);

            //Act
            var result = validationService.ValidateOrders(It.IsAny<string>(), ordersRequest);

            //Assert
            Assert.False(result.Success);
        }

        [Theory]
        [InlineAutoData(ValidationState.Success)]
        public void ValidateRequest_ReturnsSuccessTrue_WhenPortfolioValidationServiceReturnsSuccessfulValidationState(ValidationState validationState, List<Stock> stocks)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ValidationService>>();

            var mockPortfolioValidationService = new Mock<IPortfolioValidationService>();

            var validationService = new ValidationService(mockLogger.Object, mockPortfolioValidationService.Object);

            var ordersRequest = new OrdersRequest() { Stocks = stocks };
            var portfolio = new Portfolio { stocks };

            mockPortfolioValidationService.Setup(x => x.ValidatePortfolio(portfolio, It.IsAny<string>()))
                .Returns(validationState);

            //Act
            var result = validationService.ValidateOrders(It.IsAny<string>(), ordersRequest);

            //Assert
            Assert.True(result.Success);
        }
    }
}
