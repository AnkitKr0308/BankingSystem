using BankingSystem.Application.DTOs.Transactions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Validators.Transaction
{
    public class TransactionValidator : AbstractValidator<CreateTransactionDTO>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Transaction Amount should be greater than zero")

                .LessThanOrEqualTo(100000)
                .WithMessage("Maximum Transaction Limit is 100000");

            //RuleFor(x => x.Type)
            //    .IsInEnum()
            //    .WithMessage("Invalid Transaction Type");

        }
    }
}
