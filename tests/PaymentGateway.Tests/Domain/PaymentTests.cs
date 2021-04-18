using System;
using FluentAssertions;
using PaymentGateway.Models.Domain;
using Xunit;

namespace PaymentGateway.Tests.Domain
{
    public class PaymentTests
    {
        [Theory]
        [InlineData("1234-5678-9101-1121", "XXXX-XXXX-XXXX-1121")]
        [InlineData("1234 5678 9101 1121", "XXXX XXXX XXXX 1121")]
        [InlineData("1234567891011121", "XXXXXXXXXXXX1121")]
        public void WhenCardNumberPropertyIsAccessed_ReturnsCardNumberWithOnlyFourLastDigitsVisible(string given, string expected)
        {
            var target = new Payment
            {
                CardNumber = given
            };

            target.CardNumber.Should().Be(expected);
        }
    }
}