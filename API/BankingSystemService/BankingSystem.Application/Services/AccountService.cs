using BankingSystem.Application.DTOs;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IRepository<Transaction> _transactionRepo;
        public AccountService(IAccountRepository account, IRepository<Transaction> transaction)
        {
            _accountRepo = account;
            _transactionRepo = transaction;
        }

        public async Task<BankAccount> CreateAccountAsync(int customerId)
        {
            var account = new BankAccount
            {
                customerId=customerId,
                AccountNumber = Guid.NewGuid().ToString()[..10],
                Balance = 0
            };

            await _accountRepo.AddAsync(account);
            await _accountRepo.SaveChangesAsync();
            return account;            
        }

        public async Task DepositAsync(int accountId, decimal amount)
        {
            var account = await _accountRepo.GetByIdAsync(accountId);

            if (account==null)
                throw new Exception($"Account Number {accountId} does not exist");

            if (amount <= 0)
                throw new Exception("Amount should be greater than zero.");

            account.Balance += amount;
            _accountRepo.Update(account);

            var transaction = new Transaction
            {
                Amount=amount,
                Type="Deposit",
                BankAccountId = accountId
            };
            await _transactionRepo.AddAsync(transaction);
            await _accountRepo.SaveChangesAsync();
        }

        public async Task<AccountDetailsDTO?> GetAccountAsync(int accountId)
        {
           var account = await _accountRepo.GetAccountDetailsAsync(accountId);

            if (account == null)
                throw new Exception($"Account Number {accountId} does not exist");

            return new AccountDetailsDTO
            {
                Id = accountId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,

                Customer = new CustomerDTO
                {
                    Id = account.Customer.Id,
                    Name = account.Customer.FirstName + account.Customer.LastName,
                    Email = account.Customer.Email
                },

                Transactions = account.Transacations.Select(t => new TransactionDTO
                {
                    Amount = t.Amount,
                    Type = t.Type,
                    CreatedAt = t.CreatedAt
                }).ToList()
            };
        }

        public async Task WithdrawAsync(int accountId, decimal amount)
        {
            var account = await _accountRepo.GetByIdAsync(accountId);

            if (account == null)
                throw new Exception($"Account Number {accountId} does not exist");

            if (amount > account.Balance)
                throw new Exception("Insufficient Balance");

            account.Balance -= amount;
            _accountRepo.Update(account);

            await _transactionRepo.AddAsync(new Transaction
            {
                Amount=amount,
                Type="Withdraw",
                BankAccountId = accountId
            });

            await _accountRepo.SaveChangesAsync();
        }
    }
}
