using BankingSystem.Application.DTOs.Transactions;
using BankingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Domain.Entity;
using FluentValidation;
using BankingSystem.Application.Validators.Transaction;
using BankingSystem.Domain.Enums;
using System.Runtime.CompilerServices;
//using BankingSystem.Infrastructure.Data.DbContext;


namespace BankingSystem.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepo;
        private readonly IRepository<TransferTransaction> _transferTransaction;
        private readonly IValidator<CreateTransactionDTO> _transactionValidator;
        private readonly IRepository<BankAccount> _account;
        private readonly IValidator<TransferDTO> _transferValidator;
       private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IRepository<Transaction> transactionRepo, 
            IRepository<TransferTransaction> transferTransaction, 
            IValidator<CreateTransactionDTO> transactionValidator,
            IRepository<BankAccount> account,
            IValidator<TransferDTO> transferValidator,
            IUnitOfWork unitOfWork)
        {
            _transactionRepo = transactionRepo;
            _transferTransaction = transferTransaction;
            _transactionValidator = transactionValidator;
            _account = account;
            _transferValidator = transferValidator;
           _unitOfWork = unitOfWork;
        }
        public async Task DepositAsync(CreateTransactionDTO transactionDTO)
        {
            var validationResult = await _transactionValidator.ValidateAsync(transactionDTO);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _account.FirstOrDefaultAsync(x=>x.AccountNumber == transactionDTO.AccountNumber);

            if (account == null)
                throw new KeyNotFoundException($"Account {transactionDTO.AccountNumber} not found");

            if (account.Status == Status.Closed || account.Status == Status.Frozen || account.Status == Status.Locked)
                throw new InvalidOperationException($"Account {transactionDTO.AccountNumber} is {account.Status}, please get account activated to enable transactions.");

            account.Balance += transactionDTO.Amount;

            _account.Update(account);

            var transaction = new Transaction
            {
                Amount = transactionDTO.Amount,
                TransactionType=TransactionType.Deposit,
                BankAccountId = account.Id
            };

            await _transactionRepo.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<IEnumerable<TransactionDetailsDTO>> GetTransactionHistoryAsync(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public async Task TransferAsync(TransferDTO transferDTO)
        {
            //throw new NotImplementedException();

            var validationResult = await _transferValidator.ValidateAsync(transferDTO);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sender = await _account.FirstOrDefaultAsync(x => x.AccountNumber == transferDTO.SenderAccountNumber);
            var receiver = await _account.FirstOrDefaultAsync(x=>x.AccountNumber == transferDTO.ReceiverAccountNumber);

            if (sender == null || receiver==null)
                throw new KeyNotFoundException($"Account not found.");


            if (sender.Balance < transferDTO.Amount)
                throw new InvalidOperationException("Insufficient balance.");

            var transfer = new TransferTransaction
            {
                TransferReference = "T"+Guid.NewGuid().ToString("N"),
                SenderAccountId=sender.Id,
                ReceiverAccountId=receiver.Id,
                Amount=transferDTO.Amount,
                Status=TransferStatus.Pending
            };

            await _transferTransaction.AddAsync(transfer);

           

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                transfer.Status = TransferStatus.Processing;

                sender.Balance -= transferDTO.Amount;
                receiver.Balance += transferDTO.Amount;

                var senderTransaction = new Transaction
                {
                    Amount = transferDTO.Amount,
                    BankAccountId = sender.Id,
                    TransactionType = TransactionType.Debit
                };

                var receiverTransaction = new Transaction
                {
                    Amount = transferDTO.Amount,
                    BankAccountId = receiver.Id,
                    TransactionType = TransactionType.Credit
                };

                _account.Update(sender);
                _account.Update(receiver);
               
                await _transactionRepo.AddAsync(senderTransaction);
                await _transactionRepo.AddAsync(receiverTransaction);

                transfer.Status = TransferStatus.Completed;



                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex) {
                await _unitOfWork.RollbackAsync();

                transfer.Status = TransferStatus.Failed;
                transfer.FailureReason = ex.Message;

                _transferTransaction.Update(transfer);
                await _unitOfWork.SaveChangesAsync();

                throw;
            }

        }

        public async Task WithdrawAsync(CreateTransactionDTO transactionDTO)
        {
            var validationResult = await _transactionValidator.ValidateAsync(transactionDTO);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var account = await _account.FirstOrDefaultAsync(x=>x.AccountNumber==transactionDTO.AccountNumber);

            if (account == null)
                throw new KeyNotFoundException($"Account {transactionDTO.AccountNumber} not found.");

            if (account.Status == Status.Frozen || account.Status == Status.Locked || account.Status == Status.Closed)
                throw new InvalidOperationException($"Account {transactionDTO.AccountNumber} is {account.Status}, please get it Active to initiate transaction.");

            account.Balance-= transactionDTO.Amount;

            _account.Update(account);

            var transaction = new Transaction
            {
                Amount = transactionDTO.Amount,
                TransactionType = TransactionType.Withdraw,
                BankAccountId = account.Id
            };

            await _transactionRepo.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
