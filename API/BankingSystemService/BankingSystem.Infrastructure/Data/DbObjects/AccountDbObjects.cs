using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Data.DbObjects
{
    public static class AccountDbObjects
    {
        public static string CreateAccountNumberSequence
            => @"
                    CREATE SEQUENCE AccountNumberSequence
                    START WITH 10800000
                    INCREMENT BY 1;
            ";

    }
}
