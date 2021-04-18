using System;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using PaymentGateway.DataAccess;
using PaymentGateway.Integrations.Clients;
using PaymentGateway.Models.Contracts;
using PaymentGateway.Models.Domain;
using PaymentGateway.Models.Enums;
using PaymentGateway.Services;
using Xbehave;

namespace PaymentGateway.Tests.Services
{
    public class PaymentServiceTests
    {
        IPaymentRepository _paymentRepository;
        IPaymentProviderFactory _paymentProviderFactory;
        IPaymentProvider _paymentProvider;
        PaymentService _target;
        Guid _merchantId;
        PaymentContract _input;
        Payment _persistedPayment;
        PaymentProcessResults _paymentProcessResults;

        public PaymentServiceTests()
        {
            _input = new PaymentContract
            {
                Amount = 120.49m,
                CardNumber = "1234-1234-1234-1234",
                Currency = Currency.GBP,
                CVV = "333",
                ExpiryYear = DateTime.Now.Year + 1,
                ExpiryMonth = DateTime.Now.Month + 1
            };

            _paymentProcessResults = new PaymentProcessResults
            {
                Success = true,
                TranscactionIdentifier = "111111111111111",
            };

            _paymentRepository = Substitute.For<IPaymentRepository>();
            _paymentProvider = Substitute.For<IPaymentProvider>();
            _paymentProviderFactory = Substitute.For<IPaymentProviderFactory>();

            _paymentRepository.Create(Arg.Do<Payment>(p => _persistedPayment = p));
            _paymentProvider.ProcessPayment(Arg.Any<Payment>()).Returns(_paymentProcessResults);
            _paymentProviderFactory.Create(Arg.Any<string>()).Returns(_paymentProvider);

            _target = new PaymentService(_paymentRepository, new BusinessRulesValidator(), _paymentProviderFactory);
        }

        [Background]
        public void AuthenticatedUser()
        {
            "Given an authenticated user"
                .x(() => _merchantId = Guid.NewGuid());
        }

        [Scenario]
        public void MakePaymentSuccess((bool Success, PaymentContract Data, string Errors) response)
        {
            "And the input is valid"
                .x(() => { });
            "And payment provider processes transaction successfully"
                .x(() => { });
            "When make payment is invoked"
                .x(async () => response = await _target.MakePayment(_input, _merchantId));
            "The payment details should be stored to database with succeeded status"
                .x(() =>
                {
                    _paymentRepository.Received().Create(Arg.Any<Payment>());
                    _persistedPayment.Status.Should().Be(PaymentStatus.Succeeded);
                });
            "Then response should be successful and errors are empty"
                .x(() =>
                {
                    response.Success.Should().BeTrue();
                    response.Errors.Should().BeEmpty();
                });
        }

        [Scenario]
        public void MakePaymentFailedDueToNegativeAmount((bool Success, PaymentContract Data, string Errors) response)
        {
            "And the input has invalid negative Amount"
                .x(() => _input = new PaymentContract
                {
                    CVV = "333",
                    Amount = -120.49m
                });
            "When make payment is invoked"
                .x(async () => response = await _target.MakePayment(_input, _merchantId));
            "Then response should be failed and include error about negative amount"
                .x(() =>
                {
                    response.Success.Should().BeFalse();
                    response.Errors.Should().Contain("Amount cannot be negative");
                });
        }

        [Scenario]
        public void MakePaymentFailedDueToExpiredCard((bool Success, PaymentContract Data, string Errors) response)
        {
            "And the input has invalid negative Amount"
                .x(() => _input = new PaymentContract
                {
                    CVV = "333",
                    ExpiryYear = 2000,
                    ExpiryMonth = 1
                });
            "When make payment is invoked"
                .x(async () => response = await _target.MakePayment(_input, _merchantId));

            "Then response should be failed and include error about negative amount"
                .x(() =>
                {
                    response.Success.Should().BeFalse();
                    response.Errors.Should().Contain("The card has expired");
                });
        }

        [Scenario]
        [Example("Payment rejected. Credit card is blocked.")]
        public void MakePaymentFailedDueToBlockedCard(string reason, (bool Success, PaymentContract Data, string Errors) response)
        {
            "And the input is valid"
                .x(() => { });
            $"When payment provider reject payment due to {reason}"
                .x(() =>
                {
                    _paymentProcessResults.Success = false;
                    _paymentProcessResults.AddRejectionReasons(new[] { reason });
                });
            "When make payment is invoked"
                .x(async () => response = await _target.MakePayment(_input, _merchantId));

            $"Then response should be failed and include error about {reason}"
                .x(() =>
                {
                    response.Success.Should().BeFalse();
                    response.Errors.Should().Contain(reason);
                });
        }

    }
}