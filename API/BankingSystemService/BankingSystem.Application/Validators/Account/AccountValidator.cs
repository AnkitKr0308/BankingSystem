using BankingSystem.Application.DTOs.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Validators.Account
{
    public class AccountValidator : AbstractValidator<CreateAccountDTO>
    {
        public AccountValidator() 
        { 
            //RuleFor(x=>x.AccountNumber)
            //    .Matches(@"^\d$")
            //    .MinimumLength(8);
        }
    }
}
