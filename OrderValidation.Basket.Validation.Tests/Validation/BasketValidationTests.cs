using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using Moq;
using OrderValidation.Common;
using Xunit;

namespace OrderValidation.Basket.Tests.Validation
{
    public class BasketValidationTests
    {
        /*[Theory, AutoData]
        public void IsValidBasketReturnsFalse_WhenChildOrderInChildOrder_HasNegativeWeight(
            Mock<IBasket> mockBasket,
            BasketValidationService sut,
            List<ChildOrder> childOrders,
            decimal weight)
        {
            childOrders.Add(new ChildOrder(){Weight = -1*weight});

            mockBasket.Setup(x => x.GetChildOrders()).Returns(childOrders);
        }*/
    }
}
/*var e = Record.Exception(
    () => sut.UpdateCounterDocument(document, counterId, delta, countersConfig));

Assert.NotNull(e);
Assert.IsType<CounterManipulateInvalidCounterUpdateException>(e);*/