using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using AutoFixture.Xunit2;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.Basket.Validation;
using OrderValidation.Common;
using Xunit;
using OrderValidation.Tests.Common.Extensions;
using AutoFixture.AutoMoq;

namespace OrderValidation.Basket.Tests.Validation
{
    public class PortfolioValidationTests
    {
        [Fact]
        public void ValidateBasket_ReturnsEmptyBasketState_WhenNoStocksInBasket()
        {
            //Arrange
            var basket = new Portfolio() {new List<Stock>()};
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);

            //Assert
            Assert.Equal(ValidationState.EmptyPortfolio, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();

        }
        [Theory, AutoData]
        public void ValidateBasket_ReturnsNegativeStockWeightState_WhenSingleStockWeight_HasNegativeWeight(Stock Stock)
        {
            //Arrange
            var basket = new Portfolio(){ Stock };
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            basket[0].Weight *= -1;
            
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.NegativeStockWeight, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
        }

        [Theory, AutoData]
        public void ValidateBasket_ReturnsInvalidWeightState_WhenSumOfSingleStockWeight_IsNotEqualTo1(Stock Stock)
        {
            //Arrange
            var basket = new Portfolio() {Stock};
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
        }

        [Fact]
        public void ValidateBasket_ReturnsSuccessState_WhenSumOfSingleStockWeight_IsEqualTo1()
        {
            //Arrange
            var Stock = new Stock() {Weight = 1.0m};
            var basket = new Portfolio() { Stock };
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);

            //Assert
            Assert.Equal(ValidationState.Success, result);
            mockLogger.VerifyTraceWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        } 
        
        [Theory, AutoData]
        public void ValidateBasket_ReturnsNegativeStockWeight_WhenStockWeight_HasNegativeWeight(List<Stock> stocks)
        {
            //Arrange
            var basket = new Portfolio(){stocks};
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            basket[0].Weight *= -1;
            
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.NegativeStockWeight, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        }

        [Theory, AutoData]
        public void ValidateBasket_ReturnsInvalidWeightState_WhenSumOfStockWeight_IsNotEqualTo1(List<Stock> stocks)
        {
            //Arrange
            var basket = new Portfolio() {stocks};
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        }

        [Fact]
        public void ValidateBasket_ReturnsSuccessState_WhenSumOfStockWeight_IsEqualTo1()
        {
            //Arrange
            var basket = new Portfolio() { new List<Stock>()
            {
                new Stock(){Weight = 0.2m },
                new Stock(){Weight = 0.2m },
                new Stock(){Weight = 0.2m },
                new Stock(){Weight = 0.2m },
                new Stock(){Weight = 0.2m },
            }
            };
            
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);

            //Assert
            Assert.Equal(ValidationState.Success, result);
            mockLogger.VerifyTraceWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        }

    }
}
