using AutoFixture.Xunit2;
using Xunit;

namespace OrderValidation.Currency.Validation.Tests
{
    public class CurrencyValidationServiceTests
    {
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsFalse_WhenCurrencyIsNotSupported(string currency, CurrencyValidationService sut)
        {
            //Arrange
            //Act
            var result = sut.IsSupportedCurrency(currency);
            
            //Assert
            Assert.False(result);
        }
        
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsTrue_WhenCurrencyIsSupported_WithHKD(CurrencyValidationService sut)
        {
            //Arrange
            //Act
            var result = sut.IsSupportedCurrency("HKD");
            
            //Assert
            Assert.True(result);
        }
        
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsTrue_WhenCurrencyIsSupported_WithUSD(CurrencyValidationService sut)
        {
            //Arrange
            //Act
            var result = sut.IsSupportedCurrency("USD");
            
            //Assert
            Assert.True(result);
        }
        
    }
}
