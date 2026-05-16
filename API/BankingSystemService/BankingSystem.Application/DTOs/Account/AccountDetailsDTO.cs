using BankingSystem.Application.DTOs.Customer;
using BankingSystem.Application.DTOs.Transactions;
using BankingSystem.Domain.Entity;
using BankingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Account
{
    public class AccountDetailsDTO
    {
        //public int Id { get; set; }
        public string AccountNumber { get; set; }
        public BankAccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        
        public CustomerBasicDTO Customer { get; set; } = new();
        public List<TransactionDetailsDTO> Transactions { get; set; } = new();
    }
}
