using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Data.DbObjects
{
    public static class TransactionDbObjects
    {
        public static string CreateTransactionIdSequence
            => @"
                CREATE SEQUENCE TransactionIdSequence
                    START WITH 100000000
                    INCREMENT BY 1;
            ";
    }
}
