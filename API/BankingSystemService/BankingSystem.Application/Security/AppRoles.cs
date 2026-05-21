using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Security
{
    public class AppRoles
    {
        public static readonly string Admin = "Admin";
        public static readonly string Customer = "Customer";
        public static readonly string Employee = "Employee";

        public static readonly string[] roles =
        {
            Admin,
            Customer,
            Employee
        };
    }
}
