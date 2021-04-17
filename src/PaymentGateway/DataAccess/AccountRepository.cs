using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;
using PaymentGateway.Domain;

namespace PaymentGateway.DataAccess
{
    public class AccountRepository : IAccountRepository
    {
        private DbOptions _config;

        public AccountRepository(DbOptions config)
        {
            _config = config;
        }

        public AccountRepository(IOptions<DbOptions> dbOptions)
        {
            _config = dbOptions.Value;
        }
        public async Task<Merchant> GetWithCredentials(string username, string password)
        {
            var sql = "SELECT * FROM merchants WHERE ClientName=@username AND Password=@password";

            using var connection = new MySqlConnection(_config.ConnectionString);
            return await connection.QuerySingleOrDefaultAsync<Merchant>(sql, new { username, password });
        }
    }
}