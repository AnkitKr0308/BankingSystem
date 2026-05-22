using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Authentication
{
    public class AuthResult
    {
        public string accessToken {  get; set; }
        public string refreshToken { get; set; }
    }
}
