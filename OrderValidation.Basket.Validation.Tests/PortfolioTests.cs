using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using OrderValidation.Common;
using Xunit;

namespace OrderValidation.Basket.Tests
{
    [Trait("Category", "Unit")]
    public class PortfolioTests
    {
        [Fact]
        public void Counts_ReturnsEmptyPortfolio_WhenPortfolioHasNoStocks()
        {
            //Arrange
            var sut = new Portfolio();
            
            //Act
            var result = sut.Count();
            
            //Assert
            Assert.Equal(0,result);

        }

        [Theory, AutoData]
        public void SumOrderWeight_ReturnsSumOfOrders_WhenPortfolioHasOrders(List<Stock> stocks)
        {
            //Arrange
            var sut = new Portfolio { stocks };
            
            var expectedWeight = stocks.Sum(x => x.Weight);
            
            //Act
            var result = sut.SumOrderWeight();
            
            //Assert
            Assert.Equal(expectedWeight, result);
        }

        [Theory, AutoData]
        public void GetStocks_ReturnsStocks_WhenPortfolioHasStocks(List<Stock> stocks)
        {
            //Arrange
            var sut = new Portfolio() { stocks };

            //Act
            var result = sut.GetStocks();

            //Assert
            Assert.Equal(stocks, result);
        }

    }
}
