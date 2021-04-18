using System;
using System.Threading.Tasks;
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
        public async Task<Models.Domain.Payment> GetPayment(Guid paymentId)
        {
            var payment = await _paymentRepository.Load(paymentId);
            return payment;
        }

        public async Task<(bool Success, Payment Data, string Errors)> MakePayment(Payment payment, Guid merchantId)
        {
            var validationErrors = string.Join("\n", _businessRulesValidator.Validate(payment));

            if (validationErrors.Length > 0)
                return (false, payment, validationErrors);

            payment.Merchant = new Merchant { MerchantId = merchantId };
            await _paymentRepository.Create(payment);

            // Little silly business rule that allows the choice of payment provider
            var providerName = (payment.CardNumber.Trim()[0] - '4') > 0 ? "LehmanSisters" : "DebitSuisse";
            var paymentProvider = _paymentProviderFactory.Create(providerName);

            var paymentProcessResults = await paymentProvider.ProcessPayment(payment);

            payment.Status = paymentProcessResults.Success ? PaymentStatus.Succeeded : PaymentStatus.Failed;
            payment.Transaction = paymentProcessResults.TranscactionIdentifier;
            payment.RejectionReasons = string.Join(Environment.NewLine, paymentProcessResults.RejectionReasons);
            await _paymentRepository.Update(payment);

            return (paymentProcessResults.Success, payment, payment.RejectionReasons);
        }
    }
}