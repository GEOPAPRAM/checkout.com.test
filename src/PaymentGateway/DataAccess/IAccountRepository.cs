using System.Threading.Tasks;
using PaymentGateway.Domain;

namespace PaymentGateway.DataAccess
{
    public interface IAccountRepository
    {
        Task<Merchant> GetWithCredentials(string username, string password);
    }
}