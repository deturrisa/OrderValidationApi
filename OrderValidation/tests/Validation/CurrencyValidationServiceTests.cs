using AutoFixture.Xunit2;
using OrderValidation.Common;
using OrderValidation.Currency.Validation;
using Xunit;

namespace OrderValidation.Currency.Tests.Validation
{
    [Trait("Category", "Unit")]
    public class CurrencyValidationServiceTests
    {
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsUnsupportedCurrencyState_WhenCurrencyIsNotSupported(string currency, CurrencyValidationService sut)
        {
            //Arrange
            //Act
            var result = sut.ValidateCurrency(currency);
            
            //Assert
            Assert.Equal(ValidationState.UnsupportedCurrency, result);
        }
        
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsSuccessState_WhenCurrencyIsSupported_WithHKD(CurrencyValidationService sut)
        {
            //Arrange
            //Act
            var result = sut.ValidateCurrency("HKD");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }
        
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsSuccessState_WhenCurrencyIsSupported_WithUSD(CurrencyValidationService sut)
        {
            //Arrange
            //Act
            var result = sut.ValidateCurrency("USD");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }
        
        //TODO: Test for symbols and full ISO currency format check
        
    }
}
