using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Customer
{
    public class CreateCustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Address { get; set; }
        public string ZipCode {  get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
