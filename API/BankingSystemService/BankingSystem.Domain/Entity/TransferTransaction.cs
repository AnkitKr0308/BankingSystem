using BankingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Domain.Entity
{
    public class TransferTransaction
    {
        public int Id { get; set; }
        public string TransferReference { get; set; }
        public int SenderAccountId { get; set; }
        public int ReceiverAccountId { get; set; }
        public decimal Amount { get; set; }
        public TransferStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? FailureReason { get; set; }
        public BankAccount SenderAccount {  get; set; }
        public BankAccount ReceiverAccount { get; set; }
    }
}
