using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using PaymentGateway.Models.Contracts;
using PaymentGateway.DataAccess;
using PaymentGateway.Services;
using Xunit;

namespace PaymentGateway.Tests.Validation.Services
{
    public class PaymentServiceTests
    {
        IPaymentRepository _paymentRepository;
        PaymentService _target;

        public PaymentServiceTests()
        {
            _paymentRepository = Substitute.For<IPaymentRepository>();
        }

        [Fact]
        public async Task WhenInputIsValid_MakePaymentReturnsTrueWithNoErrors()
        {
            _target = new PaymentService(_paymentRepository, new BusinessRulesValidator());

            var response = await _target.MakePayment(new PaymentContract
            {
                CardNumber = "1234-1234-1234-1234",
                CVV = "333",
                Amount = 120.49m,
                ExpiryYear = DateTime.Now.Year + 1,
                ExpiryMonth = DateTime.Now.Month + 1
            });

            response.Success.Should().BeTrue();
            response.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task WhenInputIsInvalid_MakePaymentReturnsFalseWithErrors()
        {
            _target = new PaymentService(_paymentRepository, new BusinessRulesValidator());

            var response = await _target.MakePayment(new PaymentContract
            {
                CVV = "333",
                Amount = -120.49m,
                ExpiryYear = 2000
            });

            response.Success.Should().BeFalse();
            response.Errors.Should().Contain("The card has expired");
            response.Errors.Should().Contain("Amount cannot be negative");
        }
    }
}