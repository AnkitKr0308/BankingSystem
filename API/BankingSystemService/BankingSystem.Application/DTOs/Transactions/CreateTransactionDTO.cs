using BankingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Transactions
{
    public class CreateTransactionDTO
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
