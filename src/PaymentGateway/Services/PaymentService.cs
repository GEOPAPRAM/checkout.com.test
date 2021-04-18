using System;
using System.Threading.Tasks;
using PaymentGateway.Models.Contracts;
using PaymentGateway.DataAccess;
using PaymentGateway.Models.Domain;
using PaymentGateway.Models.Enums;
using PaymentGateway.Integrations.Clients;

namespace PaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBusinessRulesValidator _businessRulesValidator;
        private readonly IPaymentProviderFactory _paymentProviderFactory;

        public PaymentService(IPaymentRepository paymentRepository,
                              IBusinessRulesValidator businessRulesValidator,
                              IPaymentProviderFactory paymentProviderFactory)
        {
            _paymentProviderFactory = paymentProviderFactory;
            _businessRulesValidator = businessRulesValidator;
            _paymentRepository = paymentRepository;
        }
        public async Task<PaymentContract> GetPayment(Guid paymentId)
        {
            var payment = await _paymentRepository.Load(paymentId);
            return payment == null ? default(PaymentContract) : ToContract(payment);
        }

        public async Task<(bool Success, PaymentContract Data, string Errors)> MakePayment(PaymentContract paymentContract, Guid merchantId)
        {
            var validationErrors = string.Join("\n", _businessRulesValidator.Validate(paymentContract));

            var payment = ToDomain(paymentContract, merchantId);

            if (validationErrors.Length > 0)
                return (false, paymentContract, validationErrors);

            await _paymentRepository.Create(payment);

            // Little silly business rule that allows the choice of payment provider
            var providerName = (payment.CardNumber.Trim()[0] - '4') > 0 ? "LehmanSisters" : "DebitSuisse";
            var paymentProvider = _paymentProviderFactory.Create(providerName);

            var paymentProcessResults = await paymentProvider.ProcessPayment(payment);

            payment.Status = paymentProcessResults.Success ? PaymentStatus.Succeeded : PaymentStatus.Failed;
            payment.Transaction = paymentProcessResults.TranscactionIdentifier;
            payment.RejectionReasons = string.Join(Environment.NewLine, paymentProcessResults.RejectionReasons);
            await _paymentRepository.Update(payment);

            var payload = ToContract(payment);

            return (paymentProcessResults.Success, payload, payment.RejectionReasons);
        }

        private PaymentContract ToContract(Payment payment)
        {
            return new PaymentContract
            {
                Id = payment.PaymentId,
                Date = payment.Date,
                Amount = payment.Amount,
                Currency = payment.Currency,
                CardNumber = payment.CardNumber,
                CVV = payment.CVV.ToString(),
                ExpiryMonth = payment.ExpiryMonth,
                ExpiryYear = payment.ExpiryYear
            };
        }

        private Payment ToDomain(PaymentContract paymentContract, Guid merchantId, Guid? paymentId = default(Guid?))
        {
            return new Payment
            {
                PaymentId = paymentId.HasValue ? paymentId.Value : Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Amount = paymentContract.Amount,
                Currency = paymentContract.Currency,
                CardNumber = paymentContract.CardNumber,
                CVV = int.Parse(paymentContract.CVV),
                ExpiryMonth = paymentContract.ExpiryMonth,
                ExpiryYear = paymentContract.ExpiryYear,
                Status = PaymentStatus.Pending,
                Merchant = new Merchant { MerchantId = merchantId }
            };
        }
    }
}