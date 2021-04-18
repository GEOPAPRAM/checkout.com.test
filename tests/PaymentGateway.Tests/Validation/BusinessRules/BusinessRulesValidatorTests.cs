using System;
using FluentAssertions;
using PaymentGateway.Models.Contracts;
using PaymentGateway.Services;
using Xunit;

namespace PaymentGateway.Tests.Validation.BusinessRules
{
    public class BusinessRulesValidatorTests
    {
        IBusinessRulesValidator _target;
        public BusinessRulesValidatorTests()
        {
            _target = new BusinessRulesValidator();
        }

        [Fact]
        public void WhenInputIsValid_ReturnsNoErrors()
        {
            var response = _target.Validate(new PaymentContract
            {
                CardNumber = "1234-1234-1234-1234",
                CVV = "333",
                Amount = 120.49m,
                ExpiryYear = DateTime.Now.Year + 1,
                ExpiryMonth = DateTime.Now.Month + 1
            });

            response.Should().BeEmpty();
        }

        [Fact]
        public void WhenInputIsInvalid_ReturnsRuleValidationErrors()
        {

            var response = _target.Validate(new PaymentContract
            {
                CVV = "333",
                Amount = -120.49m,
                ExpiryYear = 2000
            });

            response.Should().Contain("The card has expired");
            response.Should().Contain("Amount cannot be negative");
        }
    }
}