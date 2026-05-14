using BankingSystem.Application.DTOs;
using BankingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDetailsDTO?> GetAccountAsync(int accountId);
        Task DepositAsync(int accountId, decimal  amount);
        Task WithdrawAsync (int accountId, decimal amount);
        Task<BankAccount> CreateAccountAsync(int customerId);
    }
}
