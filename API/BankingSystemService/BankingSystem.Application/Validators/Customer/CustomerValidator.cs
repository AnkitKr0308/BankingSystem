using BankingSystem.Application.DTOs.Customer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Validators.Customer
{
    public class CustomerValidator : AbstractValidator<CreateCustomerDTO>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d{10}$");

            RuleFor(x => x.ZipCode)
                .Matches(@"^\d{6}$");

            //RuleFor(x => x.CustomerID)
            //    .Matches(@"^\d$")
            //    .MinimumLength(6);
        }
    }
}
