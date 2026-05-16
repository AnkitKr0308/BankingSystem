using BankingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Transactions
{
    public class TransactionDetailsDTO
    {
        public string TransactionID { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
