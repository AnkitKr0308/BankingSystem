using BankingSystem.Application.DTOs;
using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Customer;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity;
using BankingSystem.Domain.Enums;
using FluentValidation;
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
        private readonly IValidator<CreateTransactionDTO> _transactionValidator;
        public AccountService(IAccountRepository account, 
            IRepository<Transaction> transaction, 
            IValidator<CreateTransactionDTO> transactionValidator)
        {
            _accountRepo = account;
            _transactionRepo = transaction;
            _transactionValidator = transactionValidator;
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

        public async Task DepositAsync(int accountId, CreateTransactionDTO depositDTO)
        {

            var validationResult = await _transactionValidator.ValidateAsync(depositDTO);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _accountRepo.GetByIdAsync(accountId);

            if (account==null)
                throw new KeyNotFoundException($"Account Number {accountId} does not exist");


            account.Balance += depositDTO.Amount;
            _accountRepo.Update(account);

            var transaction = new Transaction
            {
                Amount= depositDTO.Amount,
                TransactionType=TransactionType.Deposit,
                BankAccountId = accountId
            };
            await _transactionRepo.AddAsync(transaction);
            await _accountRepo.SaveChangesAsync();
        }

        public async Task WithdrawAsync(int accountId, CreateTransactionDTO withdrawDTO)
        {
            var validationResult = await _transactionValidator.ValidateAsync(withdrawDTO);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _accountRepo.GetByIdAsync(accountId);

            if (account == null)
                throw new KeyNotFoundException($"Account Number {accountId} does not exist");

            if (withdrawDTO.Amount > account.Balance)
                throw new InvalidOperationException("Insufficient Balance");

            account.Balance -= withdrawDTO.Amount;
            _accountRepo.Update(account);

            await _transactionRepo.AddAsync(new Transaction
            {
                Amount = withdrawDTO.Amount,
                TransactionType = TransactionType.Withdraw,
                BankAccountId = accountId
            });

            await _accountRepo.SaveChangesAsync();
        }

        public async Task<AccountDetailsDTO?> GetAccountAsync(int accountId)
        {
           var account = await _accountRepo.GetAccountDetailsAsync(accountId);

            if (account == null)
                throw new KeyNotFoundException($"Account Number {accountId} does not exist");

            return new AccountDetailsDTO
            {
                Id = accountId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                AccountType= account.AccountType,

                Customer = new CustomerDTO
                {
                    Id = account.Customer.Id,
                    Name = account.Customer.FirstName + account.Customer.LastName,
                    Email = account.Customer.Email
                },

                Transactions = account.Transactions.Select(t => new TransactionDetailsDTO
                {
                    Amount = t.Amount,
                    Type = t.TransactionType,
                    CreatedAt = t.CreatedAt
                }).ToList()
            };
        }

       
    }
}
