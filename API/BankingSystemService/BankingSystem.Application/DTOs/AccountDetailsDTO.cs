using BankingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs
{
    public class AccountDetailsDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        
        public CustomerDTO Customer { get; set; } = new();
        public List<TransactionDTO> Transactions { get; set; } = new();
    }
}
