

namespace BankingSystem.Domain.Entity
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public int customerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Transaction> Transacations { get; set; } = new List<Transaction>();
    }
}