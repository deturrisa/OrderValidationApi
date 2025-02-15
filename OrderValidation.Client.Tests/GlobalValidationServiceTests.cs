using OrderValidation.Client.Global;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OrderValidation.Common;
using Xunit;
using Moq;
using OrderValidation.ChildOrder.Validation;
using OrderValidation.Currency.Validation;

namespace OrderValidation.Client.Tests
{
    [Trait("Category", "Unit")]
    public class GlobalValidationServiceTests
    {
        [Fact]
        public void ValidatePortfolioHasStock_ReturnsEmptyPortfolioState_WhenNoStocksInPortfolio()
        {
            //Arrange
            var portfolio = new Portfolio() { new List<Stock>() };
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);


            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidatePortfolioHasStock(portfolio);

            //Assert
            Assert.Equal(ValidationState.EmptyPortfolio, result);

        }

        [Fact]
        public void ValidateStock_ReturnsInvalidCurrencyFormat_WhenCurrencyValidationService_Returns_InvalidCurrencyFormat()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = It.Is<int>(x => x > 0) ,Symbol = "$"};

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.InvalidCurrencyFormat);

            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.InvalidCurrencyFormat, result);

        }

        [Fact]
        public void ValidateStock_ReturnsInvalidOrderIdState_WhenStockIdValidationService_Returns_InvalidInvalidOrderId()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = It.Is<int>(x => x>0), Symbol = "$" };

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.Success);

            mockStockIdValidationService.Setup(x => x.ValidateOrderId(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ValidationState.InvalidOrderId);

            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.InvalidOrderId, result);

        }

        [Fact]
        public void ValidateStock_ReturnsNegativeStockWeightState_WhenStockWeightIsNegative()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = -1, Symbol = "$" };

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.Success);

            mockStockIdValidationService.Setup(x => x.ValidateOrderId(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(ValidationState.InvalidOrderId);

            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.NegativeStockWeight, result);

        }

        [Fact]
        public void ValidateStock_ReturnsNegativeNotionalAmountState_WhenNotionalAmountIsNegative()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = It.Is<int>(x => x > 0),NotionalAmount = -1, Symbol = "$" };

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.Success);

            mockStockIdValidationService.Setup(x => x.ValidateOrderId(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ValidationState.Success);

            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.NegativeNotionalWeight, result);

        }

        [Fact]
        public void ValidateStock_ReturnsInvalidCurrencySymbolState_WhenCurrencySymbolIsNotSupported()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = It.Is<int>(x => x > 0), NotionalAmount = -1, Symbol = $"{Guid.NewGuid().ToString()}" };

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.Success);

            mockStockIdValidationService.Setup(x => x.ValidateOrderId(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ValidationState.Success);

            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.InvalidCurrencySymbol, result);

        }

        [Fact]
        public void ValidateStock_ReturnsSuccessState_WhenStockPassesGlobalValidation()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = It.Is<int>(x => x > 0), Symbol = "$" };

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.Success);

            mockStockIdValidationService.Setup(x => x.ValidateOrderId(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ValidationState.Success);

            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateStock(stock);

            //Assert
            Assert.Equal(ValidationState.Success, result);

        }

        [Fact]
        public void ValidateTotalPortfolioWeight_ReturnsInvalidWeightState_WhenTotalPortfolioWeightIsInvalid()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = -1, Symbol = "$" };

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.Success);

            mockStockIdValidationService.Setup(x => x.ValidateOrderId(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ValidationState.InvalidOrderId);

            var totalWeight = It.Is<int>(x => x != 1);
            
            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateTotalPortfolioWeight(totalWeight);

            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);

        }

        [Fact]
        public void ValidateTotalPortfolioWeight_ReturnsSuccessState_WhenTotalPortfolioWeightIs1()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<GlobalValidationService>>();
            var mockCurrencyValidationService = new Mock<ICurrencyValidationService>(MockBehavior.Strict);
            var mockStockIdValidationService = new Mock<IStockIdValidationService>(MockBehavior.Strict);
            var stock = new Stock() { Currency = It.IsAny<string>(), OrderId = $"QF-{DateTime.Now}-{It.IsAny<int>()}", Weight = -1, Symbol = "$" };

            mockCurrencyValidationService.Setup(x => x.ValidateCurrency(It.IsAny<string>()))
                .Returns(ValidationState.Success);

            mockStockIdValidationService.Setup(x => x.ValidateOrderId(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ValidationState.InvalidOrderId);

            const int totalWeight = 1;

            var sut = new GlobalValidationService(mockLogger.Object, mockCurrencyValidationService.Object, mockStockIdValidationService.Object);

            //Act
            var result = sut.ValidateTotalPortfolioWeight(totalWeight);

            //Assert
            Assert.Equal(ValidationState.Success, result);

        }

    }
}
