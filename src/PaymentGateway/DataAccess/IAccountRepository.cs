using System.Threading.Tasks;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.DataAccess
{
    public interface IAccountRepository
    {
        Task<Merchant> GetWithCredentials(string username, string password);
    }
}