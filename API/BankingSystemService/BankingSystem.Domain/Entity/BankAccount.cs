

using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entity
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public BankAccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public int customerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}