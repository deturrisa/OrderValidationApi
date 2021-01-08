using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using OrderValidation.Common;
using Xunit;

namespace OrderValidation.Basket.Tests
{
    public class PortfolioTests
    {
        [Fact]
        public void Counts_ReturnsEmptyBasket_WhenBasketHasNoStocks()
        {
            //Arrange
            var sut = new Portfolio();
            
            //Act
            var result = sut.Count();
            
            //Assert
            Assert.Equal(0,result);

        }

        [Theory, AutoData]
        public void SumOrderWeight_ReturnsSumOfOrders_WhenBasketHasOrders(List<Stock> Stocks)
        {
            //Arrange
            var sut = new Portfolio { Stocks };
            
            var expectedWeight = Stocks.Sum(x => x.Weight);
            
            //Act
            var result = sut.SumOrderWeight();
            
            //Assert
            Assert.Equal(expectedWeight, result);
        }

        [Theory, AutoData]
        public void GetStocks_ReturnsStocks_WhenBasketHasStocks(List<Stock> Stocks)
        {
            //Arrange
            var sut = new Portfolio() {Stocks};

            //Act
            var result = sut.GetStocks();

            //Assert
            Assert.Equal(Stocks, result);
        }

    }
}
