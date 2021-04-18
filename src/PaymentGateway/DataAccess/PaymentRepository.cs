using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.DataAccess
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbOptions _config;
        public PaymentRepository(DbOptions config)
        {
            _config = config;
        }

        public PaymentRepository(IOptions<DbOptions> dbOptions)
        {
            _config = dbOptions.Value;
        }

        public async Task Create(Payment payment)
        {
            var sql = $@"INSERT INTO payments (PaymentId,PaymentDate,Amount,CurrencyCode,CardNumber,CVV,ExpirationMonth,ExpirationYear,PaymentStatus,MerchantId)
            VALUES (@PaymentId, @Date, @Amount, @Currency, @CardNumber, @CVV, @ExpiryMonth, @ExpiryYear, @Status, @MerchantId);";
            using var connection = new MySqlConnection(_config.ConnectionString);
            await connection.ExecuteAsync(sql, new
            {
                payment.PaymentId,
                payment.Date,
                payment.Amount,
                payment.Currency,
                payment.CardNumber,
                payment.CVV,
                payment.ExpiryMonth,
                payment.ExpiryYear,
                payment.Status,
                payment.Merchant.MerchantId
            });
        }

        public async Task<Payment> Load(Guid paymentId)
        {
            using var connection = new MySqlConnection(_config.ConnectionString);
            return await connection.QuerySingleOrDefaultAsync<Payment>("SELECT * FROM payments WHERE PaymentId=@paymentId", new { paymentId });
        }

        public async Task Update(Payment payment)
        {
            var sql = $@"UPDATE payments SET PaymentStatus=@Status, Transaction=@Transaction, RejectionReasons=@RejectionReasons WHERE PaymentId=@PaymentId";
            using var connection = new MySqlConnection(_config.ConnectionString);
            await connection.ExecuteAsync(sql, new
            {
                payment.PaymentId,
                payment.Status,
                payment.Transaction,
                payment.RejectionReasons
            });
        }
    }
}