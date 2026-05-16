using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Data.DbObjects
{
    public static class CustomerDbObjects
    {
        public static string CreateCustomerIdSequence
            => @"
                    CREATE SEQUENCE CustomerIdSequence
                    START WITH 100000
                    INCREMENT BY 1;
            ";
    }
}
