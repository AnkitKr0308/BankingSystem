using BankingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Customer
{
    public class CustomerDetailsDTO
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public List<CustomerAccountDTO> Accounts { get; set; } = new();

    }
}
