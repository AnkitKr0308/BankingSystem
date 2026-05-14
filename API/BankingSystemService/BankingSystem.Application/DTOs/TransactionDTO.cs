using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs
{
    public class TransactionDTO
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
