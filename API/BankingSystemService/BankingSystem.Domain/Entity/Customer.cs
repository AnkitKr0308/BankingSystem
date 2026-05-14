using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Domain.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
    }
}
