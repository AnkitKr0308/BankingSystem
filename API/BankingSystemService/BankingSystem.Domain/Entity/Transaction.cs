using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entity
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }
    }
}