using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Moq;
using OrderValidation.Common.DateTimeWrapper;
using Xunit;

namespace OrderValidation.ChildOrder.Tests
{
    public class StockIdGeneratorTests
    {
        [Theory, AutoData]
        public void GenerateOrderId_ReturnsIncrementedIndexBy1AndCurrentDate_WhenOneStockInstantiated(DateTime dateTime, int index)
        {
            //Arrange
            
            var mockDateTimeWrapper = new Mock<IDateTimeWrapper>();
            mockDateTimeWrapper.Setup(x => x.Now()).Returns(dateTime);
            
            var sut = new StockIdGenerator(mockDateTimeWrapper.Object);

            //Act
            var result = sut.GenerateDateIndexId(index);
            
            //Assert
            Assert.Equal($"QF-{dateTime}-{index}",result);
            
            //TODO: fix date format to yyyy-MM-dd
        }

    }
}
