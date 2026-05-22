using BankingSystem.Application.DTOs.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Interfaces
{
    public interface ITransactionService
    {
        Task DepositAsync(CreateTransactionDTO transactionDTO);
        Task WithdrawAsync(CreateTransactionDTO transactionDTO);
        Task TransferAsync (TransferDTO transferDTO);
        Task<IEnumerable<TransactionDetailsDTO>> GetTransactionHistoryAsync(string accountNumber);
    }
}
