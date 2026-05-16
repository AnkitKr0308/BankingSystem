using BankingSystem.Application.DTOs;
using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Customer;
using BankingSystem.Application.DTOs.Transactions;
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
        private readonly IValidator<CreateAccountDTO> _accountValidator;
        private readonly ICustomerService _customerService;
        public AccountService(IAccountRepository account, 
            IRepository<Transaction> transaction, 
            IValidator<CreateTransactionDTO> transactionValidator,
            IValidator<CreateAccountDTO> accountValidator,
            ICustomerService customerService
        )
        {
            _accountRepo = account;
            _transactionRepo = transaction;
            _transactionValidator = transactionValidator;
            _accountValidator = accountValidator;
            _customerService=customerService;
        }

        public async Task<AccountDetailsDTO> CreateAccountAsync(CreateAccountDTO createAccountDTO)
        {
            //var validationResult = await _accountValidator.ValidateAsync( createAccountDTO );
            //if (!validationResult.IsValid)
            //    throw new ValidationException(validationResult.Errors);

            var customer = await _customerService.GetCustomerDataAsync(createAccountDTO.CustomerId);
            if (customer == null)
                throw new KeyNotFoundException($"Customer {createAccountDTO.CustomerId} not found");

            var account = new BankAccount
            {
                CustomerId = customer.Id,
                //AccountNumber = Guid.NewGuid().ToString()[..10],
                AccountType = createAccountDTO.AccountType,
                Balance = createAccountDTO.InitialDeposit,
                status=Status.Active
            };

            await _accountRepo.AddAsync(account);
            await _accountRepo.SaveChangesAsync();

            if (createAccountDTO.InitialDeposit > 0)
            {
                var transaction = new Transaction
                {
                    //TransactionID = GenerateTransactionId(),
                    Amount = createAccountDTO.InitialDeposit,
                    TransactionType = TransactionType.Deposit,
                    BankAccountId = account.Id
                };

                await _transactionRepo.AddAsync(transaction);
                await _transactionRepo.SaveChangesAsync();
            }

            return new AccountDetailsDTO
            {
                AccountNumber=account.AccountNumber,
                AccountType=account.AccountType,
                Balance = account.Balance
            };
        }

        public async Task DepositAsync(CreateTransactionDTO depositDTO)
        {

            var validationResult = await _transactionValidator.ValidateAsync(depositDTO);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _accountRepo.FirstOrDefaultAsync(x=>x.AccountNumber==depositDTO.AccountNumber);

            if (account==null)
                throw new KeyNotFoundException($"Account Number {depositDTO.AccountNumber} does not exist");

            
            
            account.Balance += depositDTO.Amount;
            _accountRepo.Update(account);

            var transaction = new Transaction
            {
                Amount= depositDTO.Amount,
                TransactionType=TransactionType.Deposit,
                BankAccountId = account.Id
            };
            await _transactionRepo.AddAsync(transaction);
            await _accountRepo.SaveChangesAsync();
        }

        public async Task WithdrawAsync(CreateTransactionDTO withdrawDTO)
        {
            var validationResult = await _transactionValidator.ValidateAsync(withdrawDTO);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _accountRepo.FirstOrDefaultAsync(x=>x.AccountNumber==withdrawDTO.AccountNumber);

            if (account == null)
                throw new KeyNotFoundException($"Account Number {withdrawDTO.AccountNumber} does not exist");

            if (withdrawDTO.Amount > account.Balance)
                throw new InvalidOperationException("Insufficient Balance");

            account.Balance -= withdrawDTO.Amount;
            _accountRepo.Update(account);

            await _transactionRepo.AddAsync(new Transaction
            {
                Amount = withdrawDTO.Amount,
                TransactionType = TransactionType.Withdraw,
                BankAccountId = account.Id
            });

            await _accountRepo.SaveChangesAsync();
        }

        public async Task<AccountDetailsDTO?> GetAccountAsync(string accountNumber)
        {
           var account = await _accountRepo.GetAccountDetailsAsync(accountNumber);

            if (account == null)
                throw new KeyNotFoundException($"Account Number {accountNumber} does not exist");

            var customer = await _customerService.GetCustomerDataAsync(account.Customer.CustomerId);

            return new AccountDetailsDTO
            {
                //Id = accountId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                AccountType= account.AccountType,

                Customer = new CustomerBasicDTO
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    //Email = account.Customer.Email
                },

                Transactions = account.Transactions.Select(t => new TransactionDetailsDTO
                {
                    TransactionID = t.TransactionID,
                    Amount = t.Amount,
                    Type = t.TransactionType,
                    CreatedAt = t.CreatedAt
                }).ToList()
            };
        }

       
    }
}
