using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.Common.DateTimeWrapper;
using Xunit;

namespace OrderValidation.ChildOrder.Tests
{
    public class StockIdGeneratorTests
    {
        [Theory, AutoData]
        public void GenerateOrderId_ReturnsIncrementedIndexBy1AndCurrentDate_WhenOneStockInstantiated(DateTime dateTime,
            int index,
            Mock<ILogger<StockIdGenerator>> loggerMock,
            Mock<IDateTimeWrapper> mockDateTimeWrapper)
        {
            //Arrange
            mockDateTimeWrapper.Setup(x => x.Now()).Returns(dateTime);
            
            var sut = new StockIdGenerator(mockDateTimeWrapper.Object, loggerMock.Object);

            //Act
            var result = sut.GenerateDateIndexId(index);
            
            //Assert
            Assert.Equal($"QF-{dateTime}-{index}",result);
            
            //TODO: fix date format to yyyy-MM-dd
        }

    }
}
