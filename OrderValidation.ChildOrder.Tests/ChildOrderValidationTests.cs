using AutoFixture.Xunit2;
using System;
using Xunit;

namespace OrderValidation.Stock.Tests
{
    public class StockValidationTests
    {
        [Theory, AutoData]
        public void GenerateOrderId_ReturnsIncrementedIndexBy1AndCurrentDate_WhenOneStockInstantiated()
        {
            //Arrange



        }

        [Fact]
        public void GenerateOrderId_ReturnsIncrementedIndexByNAndCurrentDate_WhenOneStockInstantiatedNTimes()
        {

        }


        //TODO: BELOW TEST SHOULD BE MOVED TO BASKET VALIDATION
        [Fact]
        public void GenerateOrderId_ThrowsException_WhenOneStockIdDateMismatch()
        {

        }

        [Fact]
        public void GenerateOrderId_ThrowsException_WhenOneStockInstantiated_ButIdNotIncremented()
        {

        }

        [Fact]
        public void GenerateOrderId_ReturnsIndex_AfterStockInstantiatedNTimes_AndDateChanges()
        {

        }

        [Fact]
        public void GenerateOrderId_ReturnsResetIndex_AfterStockInstantiatedNTimes_AndDateChanges()
        {

        }

    }
}