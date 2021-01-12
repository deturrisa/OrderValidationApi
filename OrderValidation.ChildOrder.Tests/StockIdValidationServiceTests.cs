using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.ChildOrder.Validation;
using OrderValidation.Common;
using OrderValidation.Common.DateTimeWrapper;
using System;
using Xunit;

namespace OrderValidation.ChildOrder.Tests
{
    public class StockIdValidationServiceTests
    {
        [Theory, AutoData]
        public void ValidateOrderId_ReturnsSuccessState_WhenOrderId_IsEqualToDateTimeNow_WithFirstOrder(
            DateTime dateTime, int index)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<StockIdValidationService>>();
            var mockDateTimeWrapper = new Mock<IDateTimeWrapper>(MockBehavior.Strict);

            mockDateTimeWrapper.Setup(x => x.Now()).Returns(dateTime);

            var expectedDate = $"{dateTime.Day:00}/{dateTime.Month:00}/{dateTime.Year}";

            var orderId = $"QF-{expectedDate}-{index}";

            var sut = new StockIdValidationService(mockDateTimeWrapper.Object, mockLogger.Object);

            //Act
            var result = sut.ValidateOrderId(orderId,0);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

        [Theory, AutoData]
        public void ValidateOrderId_ReturnsInvalidOrderIdState_WhenOrderIdDate_IsNotEqualToDateTimeNow(DateTime dateTime, int index)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<StockIdValidationService>>();
            var mockDateTimeWrapper = new Mock<IDateTimeWrapper>(MockBehavior.Strict);

            mockDateTimeWrapper.Setup(x => x.Now()).Returns(dateTime.AddDays(1));

            var expectedDate = $"{dateTime.Day:00}/{dateTime.Month:00}/{dateTime.Year}";

            var orderId = $"QF-{expectedDate}-{index}";

            var sut = new StockIdValidationService(mockDateTimeWrapper.Object, mockLogger.Object);

            //Act
            var result = sut.ValidateOrderId(orderId,0);

            //Assert
            Assert.Equal(ValidationState.InvalidOrderId, result);
        }

        [Theory, AutoData]
        public void
            ValidateOrderId_ReturnsInvalidOrderIdState_WhenOrderId_IsEqualToDateTimeNow_AndOrderIdIndexIsLessThanPreviousOrderIdIndex(
                DateTime dateTime,  int previousIndex)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<StockIdValidationService>>();
            var mockDateTimeWrapper = new Mock<IDateTimeWrapper>(MockBehavior.Strict);

            mockDateTimeWrapper.Setup(x => x.Now()).Returns(dateTime);

            var expectedDate = $"{dateTime.Day:00}/{dateTime.Month:00}/{dateTime.Year}";

            var currentIndex = previousIndex-1;

            var currentOrderId = $"QF-{expectedDate}-{currentIndex}";

            var sut = new StockIdValidationService(mockDateTimeWrapper.Object, mockLogger.Object);

            //Act
            var result = sut.ValidateOrderId(currentOrderId, previousIndex);

            //Assert
            Assert.Equal(ValidationState.InvalidOrderId, result);
        }

        [Theory, AutoData]
        public void
            ValidateOrderId_ReturnsInvalidOrderIdState_WhenOrderId_IsNotEqualToDateTimeNow_AndOrderIdIsLessThanPreviousOrderId(
                DateTime dateTime, int currentIndex, int previousIndex)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<StockIdValidationService>>();
            var mockDateTimeWrapper = new Mock<IDateTimeWrapper>(MockBehavior.Strict);

            mockDateTimeWrapper.Setup(x => x.Now()).Returns(new DateTime());

            var expectedDate = $"{dateTime.Day:00}/{dateTime.Month:00}/{dateTime.Year}";

            currentIndex -= previousIndex;

            var currentOrderId = $"QF-{expectedDate}-{currentIndex}";

            var sut = new StockIdValidationService(mockDateTimeWrapper.Object, mockLogger.Object);

            //Act
            var result = sut.ValidateOrderId(currentOrderId,previousIndex);

            //Assert
            Assert.Equal(ValidationState.InvalidOrderId, result);
        }

        [Fact]
        public void ValidateOrderId_ReturnsInvalidOrderIdState_WhenOrderId_IsNotCorrectFormat()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<StockIdValidationService>>();
            var mockDateTimeWrapper = new Mock<IDateTimeWrapper>(MockBehavior.Strict);

            mockDateTimeWrapper.Setup(x => x.Now()).Returns(new DateTime());

            var sut = new StockIdValidationService(mockDateTimeWrapper.Object, mockLogger.Object);

            //Act
            var result = sut.ValidateOrderId(new Guid().ToString(),0);

            //Assert
            Assert.Equal(ValidationState.InvalidOrderId, result);
        }

        [Theory, AutoData]
        public void
            ValidateOrderId_ReturnsSuccessState_WhenOrderId_IsEqualToDateTimeNow_AndOrderIdIsGreaterThanPreviousOrderId(
                DateTime dateTime, int currentIndex, int previousIndex)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<StockIdValidationService>>();
            var mockDateTimeWrapper = new Mock<IDateTimeWrapper>(MockBehavior.Strict);

            mockDateTimeWrapper.Setup(x => x.Now()).Returns(dateTime);

            var expectedDate = $"{dateTime.Day:00}/{dateTime.Month:00}/{dateTime.Year}";

            currentIndex += previousIndex;

            var currentOrderId = $"QF-{expectedDate}-{currentIndex}";

            var sut = new StockIdValidationService(mockDateTimeWrapper.Object, mockLogger.Object);

            //Act
            var result = sut.ValidateOrderId(currentOrderId,previousIndex);

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }

    }
}
