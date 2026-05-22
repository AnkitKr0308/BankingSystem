using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Transactions;
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
        Task<AccountDetailsDTO?> GetAccountAsync(string accountNumber);
        //Task DepositAsync(CreateTransactionDTO transactionDTO);
        //Task WithdrawAsync (CreateTransactionDTO transactionDTO);
        Task<AccountDetailsDTO> CreateAccountAsync(CreateAccountDTO createAccountDTO);
        Task CloseAccount(string accountNumber);
    }
}
