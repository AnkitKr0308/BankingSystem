using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.DTOs.Auth
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        //public DateTime Expiration { get; set; }
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}
