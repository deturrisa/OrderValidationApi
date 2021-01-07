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
    public class BasketValidationTests
    {
        [Fact]
        public void ValidateBasket_ReturnsEmptyBasketState_WhenNoChildOrdersInBasket()
        {
            //Arrange
            var basket = new Basket() {new List<ChildOrder>()};
            var mockLogger = new Mock<ILogger<BasketValidationService>>();

            var sut = new BasketValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);

            //Assert
            Assert.Equal(ValidationState.EmptyBasket, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();

        }
        [Theory, AutoData]
        public void ValidateBasket_ReturnsInvalidChildOrderState_WhenSingleChildOrderWeight_HasNegativeWeight(ChildOrder childOrder)
        {
            //Arrange
            var basket = new Basket(){ childOrder };
            var mockLogger = new Mock<ILogger<BasketValidationService>>();
            basket[0].Weight *= -1;
            
            var sut = new BasketValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.NegativeChildOrderWeight, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
        }

        [Theory, AutoData]
        public void ValidateBasket_ReturnsInvalidWeightState_WhenSumOfSingleChildOrderWeight_IsNotEqualTo1(ChildOrder childOrder)
        {
            //Arrange
            var basket = new Basket() {childOrder};
            var mockLogger = new Mock<ILogger<BasketValidationService>>();
            var sut = new BasketValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
        }

        [Fact]
        public void ValidateBasket_ReturnsSuccessState_WhenSumOfSingleChildOrderWeight_IsEqualTo1()
        {
            //Arrange
            var childOrder = new ChildOrder() {Weight = 1.0m};
            var basket = new Basket() { childOrder };
            var mockLogger = new Mock<ILogger<BasketValidationService>>();
            var sut = new BasketValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);

            //Assert
            Assert.Equal(ValidationState.Success, result);
            mockLogger.VerifyTraceWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        } 
        
        [Theory, AutoData]
        public void ValidateBasket_ReturnsInvalidChildOrderState_WhenChildOrderWeight_HasNegativeWeight(List<ChildOrder> childOrders)
        {
            //Arrange
            var basket = new Basket(){childOrders};
            var mockLogger = new Mock<ILogger<BasketValidationService>>();
            basket[0].Weight *= -1;
            
            var sut = new BasketValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.NegativeChildOrderWeight, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        }

        [Theory, AutoData]
        public void ValidateBasket_ReturnsInvalidWeightState_WhenSumOfChildOrderWeight_IsNotEqualTo1(List<ChildOrder> childOrders)
        {
            //Arrange
            var basket = new Basket() {childOrders};
            var mockLogger = new Mock<ILogger<BasketValidationService>>();
            var sut = new BasketValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);
            
            //Assert
            Assert.Equal(ValidationState.InvalidWeightState, result);
            mockLogger.VerifyTraceWasCalled();
            mockLogger.VerifyWarningWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        }

        [Fact]
        public void ValidateBasket_ReturnsSuccessState_WhenSumOfChildOrderWeight_IsEqualTo1()
        {
            //Arrange
            var basket = new Basket() { new List<ChildOrder>()
            {
                new ChildOrder(){Weight = 0.2m },
                new ChildOrder(){Weight = 0.2m },
                new ChildOrder(){Weight = 0.2m },
                new ChildOrder(){Weight = 0.2m },
                new ChildOrder(){Weight = 0.2m },
            }
            };
            
            var mockLogger = new Mock<ILogger<BasketValidationService>>();
            var sut = new BasketValidationService(mockLogger.Object);

            //Act
            var result = sut.ValidateBasket(basket);

            //Assert
            Assert.Equal(ValidationState.Success, result);
            mockLogger.VerifyTraceWasCalled();
            //TODO: FIXTURE FOR BASKET CLASS
        }

    }
}
