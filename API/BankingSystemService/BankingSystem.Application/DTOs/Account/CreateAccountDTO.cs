using BankingSystem.Application.DTOs.Customer;
using BankingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Account
{
    public class CreateAccountDTO
    {
        //public string AccountNumber {  get; set; }
        public string CustomerId {  get; set; }
        public BankAccountType AccountType { get; set; }
        public decimal InitialDeposit { get; set; }
    }
}
