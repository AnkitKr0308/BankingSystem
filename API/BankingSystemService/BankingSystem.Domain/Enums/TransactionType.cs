using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Domain.Enums
{
    public enum TransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Debit=3,
        Credit=4,
        Transfer = 5
    }
}
