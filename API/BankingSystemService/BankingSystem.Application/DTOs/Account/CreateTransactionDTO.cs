using BankingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Account
{
    public class CreateTransactionDTO
    {
        public decimal Amount { get; set; }
    }
}
