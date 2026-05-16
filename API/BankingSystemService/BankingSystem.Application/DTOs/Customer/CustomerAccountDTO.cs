using BankingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Customer
{
    public class CustomerAccountDTO
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        public string AccountNumber { get; set; }
        public BankAccountType AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}
