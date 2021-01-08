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
using OrderValidation.ChildOrder;

namespace OrderValidation.Basket.Tests.Validation
{
    [Trait("Category", "Unit")]
    public class PortfolioValidationTests
    {
        [Fact]
        public void ValidatePortfolio_ReturnsEmptyPortfolioState_WhenNoStocksInPortfolio()
        {
            //Arrange
            var portfolio = new Portfolio() {new List<Stock>()};
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();

            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio);

            //Assert
            Assert.Equal(ValidationState.EmptyPortfolio, result);

        }
        [Theory, AutoData]
        public void ValidatePortfolio_ReturnsNegativeStockWeightState_WhenSingleStockWeight_HasNegativeWeight(Stock stock)
        {
            //Arrange
            var portfolio = new Portfolio(){ stock };
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            portfolio[0].Weight *= -1;
            
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio);
            
            //Assert
            Assert.Equal(ValidationState.NegativeStockWeight, result);
        }

        [Theory, AutoData]
        public void ValidatePortfolio_ReturnsInvalidWeightState_WhenSumOfSingleStockWeight_IsNotEqualTo1(Stock stock)
        {
            //Arrange
            var portfolio = new Portfolio() { stock };
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio);
            
            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);
        }

        [Fact]
        public void ValidatePortfolio_ReturnsSuccessState_WhenSumOfSingleStockWeight_IsEqualTo1()
        {
            //Arrange
            var stock = new Stock() {Weight = 1.0m};
            var portfolio = new Portfolio() { stock };
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio);

            //Assert
            Assert.Equal(ValidationState.Success, result);
            //TODO: FIXTURE FOR BASKET CLASS
        } 
        
        [Theory, AutoData]
        public void ValidatePortfolio_ReturnsNegativeStockWeight_WhenStockWeight_HasNegativeWeight(List<Stock> stocks)
        {
            //Arrange
            var portfolio = new Portfolio(){stocks};
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            portfolio[0].Weight *= -1;
            
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio);
            
            //Assert
            Assert.Equal(ValidationState.NegativeStockWeight, result);
            //TODO: FIXTURE FOR BASKET CLASS
        }

        [Theory, AutoData]
        public void ValidatePortfolio_ReturnsInvalidWeightState_WhenSumOfStockWeight_IsNotEqualTo1(List<Stock> stocks)
        {
            //Arrange
            var portfolio = new Portfolio() {stocks};
            var mockLogger = new Mock<ILogger<PortfolioValidationService>>();
            var sut = new PortfolioValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidatePortfolio(portfolio);
            
            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);
            //TODO: FIXTURE FOR BASKET CLASS
        }

        [Fact]
        public void ValidatePortfolio_ReturnsSuccessState_WhenSumOfStockWeight_IsEqualTo1()
        {
            //Arrange
            var portfolio = new Portfolio() { new List<Stock>()
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
            var result = sut.ValidatePortfolio(portfolio);

            //Assert
            Assert.Equal(ValidationState.Success, result);
            //TODO: FIXTURE FOR BASKET CLASS
        }

    }
}
