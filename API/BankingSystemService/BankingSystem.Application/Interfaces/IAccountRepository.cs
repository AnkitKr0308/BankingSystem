using BankingSystem.Domain.Entity;
using System.Threading.Tasks;

namespace BankingSystem.Application.Interfaces
{
    public interface IAccountRepository : IRepository<BankAccount>
    {
        Task<BankAccount?> GetAccountDetailsAsync(int accountId);
    }
}
