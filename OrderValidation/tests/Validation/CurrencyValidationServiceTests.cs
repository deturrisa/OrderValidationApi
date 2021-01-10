using System;
using System.Linq;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Moq;
using OrderValidation.Common;
using OrderValidation.Currency.Validation;
using Xunit;

namespace OrderValidation.Currency.Tests.Validation
{
    [Trait("Category", "Unit")]
    public class CurrencyValidationServiceTests
    {
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsInvalidCurrencyFormatState_WhenCurrencyIsInvalidFormat(string currency, Mock<ILogger<CurrencyValidationService>> loggerMock)
        {
            //Arrange
            var sut = new CurrencyValidationService(loggerMock.Object);
            //Act
            var result = sut.ValidateCurrency(currency);
            
            //Assert
            Assert.Equal(ValidationState.InvalidCurrencyFormat, result);
            
        }

        [Theory, AutoData]
        public void ValidateCurrency_ReturnsUnsupportedCurrencyState_WhenCurrencyFormatIsValidAnd_CurrencyIsUnsupported(Mock<ILogger<CurrencyValidationService>> loggerMock)
        {
            //Arrange
            var sut = new CurrencyValidationService(loggerMock.Object);
            var unsupportedCurrency = "ZZZ";
            
            //Act
            var result = sut.ValidateCurrency(unsupportedCurrency);

            //Assert
            Assert.Equal(ValidationState.UnsupportedCurrency, result);
        }

        [Theory, AutoData]
        public void ValidateCurrency_ReturnsSuccessState_WhenCurrencyIsSupported_WithHKD(Mock<ILogger<CurrencyValidationService>> loggerMock)
        {
            //Arrange
            var sut = new CurrencyValidationService(loggerMock.Object);
            var result = sut.ValidateCurrency("HKD");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }
        
        [Theory, AutoData]
        public void ValidateCurrency_ReturnsSuccessState_WhenCurrencyIsSupported_WithUSD(Mock<ILogger<CurrencyValidationService>> loggerMock)
        {
            //Arrange
            var sut = new CurrencyValidationService(loggerMock.Object);
            
            //Act
            var result = sut.ValidateCurrency("USD");

            //Assert
            Assert.Equal(ValidationState.Success, result);
        }
        private string UnsupportedCurrencyHelper()
        {
            return Guid.NewGuid().ToByteArray()
                .Select(b => (byte) (((b % 16) < 10 ? 0xA : b) |
                                     (((b >> 4) < 10 ? 0xA : (b >> 4)) << 4)))
                .ToString()
                ?.Substring(4).ToUpper();
        }
        
    }
}
