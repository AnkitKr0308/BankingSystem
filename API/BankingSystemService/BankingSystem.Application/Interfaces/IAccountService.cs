using BankingSystem.Application.DTOs.Account;
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
        Task DepositAsync(int accountId, CreateTransactionDTO transactionDTO);
        Task WithdrawAsync (int accountId, CreateTransactionDTO transactionDTO);
        Task<BankAccount> CreateAccountAsync(int customerId);
    }
}
