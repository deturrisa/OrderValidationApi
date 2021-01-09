using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.Basket.Validation;
using OrderValidation.Common;
using Xunit;
using OrderValidation.Client;
using OrderValidation.Client.Clients;
using OrderValidation.Client.Global;

namespace OrderValidation.Basket.Tests.Validation
{
    [Trait("Category", "Unit")]
    public class PortfolioValidationTests
    {
        [Fact]
        public void ValidatePortfolioReturns_UnsupportedClientState_WhenClientValidationFactoryThrowsArguemntOutOfRangeException()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var mockGlobalValidationService = new Mock<IGlobalValidationService>(MockBehavior.Strict);

            var mockClientValidationFactory = new Mock<IClientValidationFactory>(MockBehavior.Strict);
            
            mockClientValidationFactory.Setup(x => x.GetClientValidation(It.IsAny<string>())).Throws<ArgumentOutOfRangeException>();
            
            var sut = new PortfolioValidationService(mockLogger.Object, mockClientValidationFactory.Object, mockGlobalValidationService.Object);

            //Act
            var result = sut.ValidatePortfolio(It.IsAny<Portfolio>(), It.IsAny<string>());

            //Assert
            Assert.Equal(ValidationState.UnsupportedClient, result);
            mockClientValidationFactory.Verify(x => x.GetClientValidation(It.IsAny<string>()));


        }

        [Fact]
        public void ValidatePortfolioReturns_EmptyPortfolioState_WhenGlobalValidationValidatesEmptyBasket()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var mockGlobalValidationService = new Mock<IGlobalValidationService>(MockBehavior.Strict);

            var mockClientValidationFactory = new Mock<IClientValidationFactory>(MockBehavior.Strict);

            mockClientValidationFactory.Setup(x => x.GetClientValidation(It.IsAny<string>()))
                .Returns(It.IsAny<IClientValidation>());

            mockGlobalValidationService.Setup(x => x.ValidatePortfolioHasStock(It.IsAny<Portfolio>()))
                .Returns(ValidationState.EmptyPortfolio);

            var sut = new PortfolioValidationService(mockLogger.Object, mockClientValidationFactory.Object, mockGlobalValidationService.Object);

            //Act
            var result = sut.ValidatePortfolio(It.IsAny<Portfolio>(), It.IsAny<string>());

            //Assert
            Assert.Equal(ValidationState.EmptyPortfolio, result);
            mockClientValidationFactory.Verify(x => x.GetClientValidation(It.IsAny<string>()));
            mockGlobalValidationService.Verify(x => x.ValidatePortfolioHasStock(It.IsAny<Portfolio>()));

        }

        [Theory,AutoData]
        public void ValidatePortfolioReturns_UnsuccessfulState_WhenGlobalValidation_FailsToValidateStock(List<Stock> stocks)
        {
            //Arrange
            var portfolio = new Portfolio() {stocks};
            
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var mockGlobalValidationService = new Mock<IGlobalValidationService>(MockBehavior.Strict);

            var mockClientValidationFactory = new Mock<IClientValidationFactory>(MockBehavior.Strict);
            
            var validationState = It.Is<ValidationState>(v => v == ValidationState.InvalidCurrencyFormat
                                                              || v == ValidationState.InvalidOrderId
                                                              || v == ValidationState.NegativeStockWeight
                                                              || v == ValidationState.NegativeNotionalWeight);

            mockClientValidationFactory.Setup(x => x.GetClientValidation(It.IsAny<string>()))
                .Returns(It.IsAny<IClientValidation>());
            
            mockGlobalValidationService.Setup(x => x.ValidatePortfolioHasStock(portfolio))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(validationState);

            var sut = new PortfolioValidationService(mockLogger.Object, mockClientValidationFactory.Object, mockGlobalValidationService.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio, It.IsAny<string>());

            //Assert
            Assert.Equal(validationState, result);
            mockClientValidationFactory.Verify(x => x.GetClientValidation(It.IsAny<string>()));
            mockGlobalValidationService.Verify(x => x.ValidatePortfolioHasStock(portfolio));
            mockGlobalValidationService.Setup(x => x.ValidateStock(It.IsAny<Stock>()));

        }

        [Theory,AutoData]
        public void ValidatePortfolioReturns_UnsuccessfulState_WhenClientValidation_FailsToValidateStock(List<Stock> stocks)
        {
            //Arrange
            var portfolio = new Portfolio() { stocks };

            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var mockGlobalValidationService = new Mock<IGlobalValidationService>(MockBehavior.Strict);

            var mockClientValidationFactory = new Mock<IClientValidationFactory>(MockBehavior.Strict);

            var mockClientValidation = new Mock<IClientValidation>(MockBehavior.Strict);

            var validationState = It.Is<ValidationState>(v => v == ValidationState.UnsupportedType
                                                              || v == ValidationState.UnsupportedCurrency
                                                              || v == ValidationState.UnsupportedDestination
                                                              || v == ValidationState.MinimumStockNotionalAmountNotMet);

            mockClientValidationFactory.Setup(x => x.GetClientValidation(It.IsAny<string>()))
                .Returns(mockClientValidation.Object);

            mockGlobalValidationService.Setup(x => x.ValidatePortfolioHasStock(portfolio))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(ValidationState.Success);

            mockClientValidation.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(validationState);

            var sut = new PortfolioValidationService(mockLogger.Object, mockClientValidationFactory.Object, mockGlobalValidationService.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio, It.IsAny<string>());

            //Assert
            Assert.Equal(validationState, result);
            mockClientValidationFactory.Verify(x => x.GetClientValidation(It.IsAny<string>()));
            mockGlobalValidationService.Verify(x => x.ValidatePortfolioHasStock(portfolio));
            mockGlobalValidationService.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
            mockClientValidation.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
        }

        [Theory,AutoData]
        public void ValidatePortfolioReturns_UnsuccessfulState_WhenGlobalValidation_FailsToValidateStockWeightTotal(List<Stock> stocks)
        {
            //Arrange
            var portfolio = new Portfolio() { stocks };

            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var mockGlobalValidationService = new Mock<IGlobalValidationService>(MockBehavior.Strict);

            var mockClientValidationFactory = new Mock<IClientValidationFactory>(MockBehavior.Strict);

            var mockClientValidation = new Mock<IClientValidation>(MockBehavior.Strict);

            mockClientValidationFactory.Setup(x => x.GetClientValidation(It.IsAny<string>()))
                .Returns(mockClientValidation.Object);

            mockGlobalValidationService.Setup(x => x.ValidatePortfolioHasStock(portfolio))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(ValidationState.Success);

            mockClientValidation.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateTotalPortfolioWeight(It.IsAny<decimal>()))
                .Returns(ValidationState.InvalidWeightState);

            var sut = new PortfolioValidationService(mockLogger.Object, mockClientValidationFactory.Object, mockGlobalValidationService.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio, It.IsAny<string>());

            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);
            mockClientValidationFactory.Verify(x => x.GetClientValidation(It.IsAny<string>()));
            mockGlobalValidationService.Verify(x => x.ValidatePortfolioHasStock(portfolio));
            mockGlobalValidationService.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
            mockClientValidation.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
            mockGlobalValidationService.Verify(x => x.ValidateTotalPortfolioWeight(It.IsAny<decimal>()));

        }

        [Theory,AutoData]
        public void ValidatePortfolioReturns_UnsuccessfulState_WhenClientValidation_FailsToValidatePortfolioNotionalAmountTotal(List<Stock> stocks)
        {
            //Arrange
            var portfolio = new Portfolio() { stocks };

            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var mockGlobalValidationService = new Mock<IGlobalValidationService>(MockBehavior.Strict);

            var mockClientValidationFactory = new Mock<IClientValidationFactory>(MockBehavior.Strict);

            var mockClientValidation = new Mock<IClientValidation>(MockBehavior.Strict);
            
            var validationState = It.Is<ValidationState>(v => v == ValidationState.MinimumPortfolioNotionalAmountNotMet
                                                              || v == ValidationState.MaximumPortfolioNotionalAmountNotMet);

            mockClientValidationFactory.Setup(x => x.GetClientValidation(It.IsAny<string>()))
                .Returns(mockClientValidation.Object);

            mockGlobalValidationService.Setup(x => x.ValidatePortfolioHasStock(portfolio))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(ValidationState.Success);

            mockClientValidation.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateTotalPortfolioWeight(It.IsAny<decimal>()))
                .Returns(ValidationState.Success);

            mockClientValidation.Setup(x => x.ValidateTotalPortfolioNotionalAmount(It.IsAny<decimal>()))
                .Returns(validationState);

            var sut = new PortfolioValidationService(mockLogger.Object, mockClientValidationFactory.Object, mockGlobalValidationService.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio, It.IsAny<string>());

            //Assert
            Assert.Equal(validationState, result);
            mockClientValidationFactory.Verify(x => x.GetClientValidation(It.IsAny<string>()));
            mockGlobalValidationService.Verify(x => x.ValidatePortfolioHasStock(portfolio));
            mockGlobalValidationService.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
            mockClientValidation.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
            mockClientValidation.Verify(x => x.ValidateTotalPortfolioNotionalAmount(It.IsAny<decimal>()));
        }

        [Theory,AutoData]
        public void ValidatePortfolioReturns_SuccessState_WhenClientValidation_AndGlobalValidation_ReturnsSuccessStates(List<Stock> stocks)
        {
            //Arrange
            var portfolio = new Portfolio() { stocks };

            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var mockGlobalValidationService = new Mock<IGlobalValidationService>(MockBehavior.Strict);

            var mockClientValidationFactory = new Mock<IClientValidationFactory>(MockBehavior.Strict);

            var mockClientValidation = new Mock<IClientValidation>(MockBehavior.Strict);

            mockClientValidationFactory.Setup(x => x.GetClientValidation(It.IsAny<string>()))
                .Returns(mockClientValidation.Object);

            mockGlobalValidationService.Setup(x => x.ValidatePortfolioHasStock(portfolio))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(ValidationState.Success);

            mockClientValidation.Setup(x => x.ValidateStock(It.IsAny<Stock>()))
                .Returns(ValidationState.Success);

            mockGlobalValidationService.Setup(x => x.ValidateTotalPortfolioWeight(It.IsAny<decimal>()))
                .Returns(ValidationState.Success);

            mockClientValidation.Setup(x => x.ValidateTotalPortfolioNotionalAmount(It.IsAny<decimal>()))
                .Returns(ValidationState.Success);

            var sut = new PortfolioValidationService(mockLogger.Object, mockClientValidationFactory.Object, mockGlobalValidationService.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio, It.IsAny<string>());

            //Assert
            Assert.Equal(ValidationState.Success, result);
            mockClientValidationFactory.Verify(x => x.GetClientValidation(It.IsAny<string>()));
            mockGlobalValidationService.Verify(x => x.ValidatePortfolioHasStock(portfolio));
            mockGlobalValidationService.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
            mockClientValidation.Verify(x => x.ValidateStock(It.IsAny<Stock>()));
            mockClientValidation.Verify(x => x.ValidateTotalPortfolioNotionalAmount(It.IsAny<decimal>()));
        }
    }
}
