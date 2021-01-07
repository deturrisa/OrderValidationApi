using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using OrderValidation.Common;
using Xunit;

namespace OrderValidation.Basket.Tests
{
    public class BasketTests
    {
        [Fact]
        public void CountChildOrders_Returns0_WhenBasketHasNoChildOrders()
        {
            //Arrange
            var sut = new Basket(new List<ChildOrder>());
            
            //Act
            var result = sut.CountChildOrders();
            
            //Assert
            Assert.Equal(0,result);

        }

        [Theory, AutoData]
        public void SumOrderWeight_ReturnsSumOfOrders_WhenBasketHasOrders(List<ChildOrder> childOrders)
        {
            //Arrange
            var sut = new Basket(childOrders);
            
            var expectedWeight = childOrders.Sum(x => x.Weight);
            
            //Act
            var result = sut.SumOrderWeight();
            
            //Assert
            Assert.Equal(expectedWeight, result);
        }

        [Theory, AutoData]
        public void GetChildOrders_ReturnsChildOrders_WhenBasketHasChildOrders(List<ChildOrder> childOrders)
        {
            //Arrange
            var sut = new Basket(childOrders);

            //Act
            var result = sut.GetChildOrders();

            //Assert
            Assert.Equal(childOrders, result);
        }

    }
}
