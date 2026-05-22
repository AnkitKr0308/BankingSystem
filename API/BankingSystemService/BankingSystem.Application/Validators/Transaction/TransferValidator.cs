using BankingSystem.Application.DTOs.Transactions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Validators.Transaction
{
    public class TransferValidator : AbstractValidator<TransferDTO>
    {
        public TransferValidator() 
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.")

                .LessThanOrEqualTo(100000)
                .WithMessage("Maximum Transaction limit is 100000");

            RuleFor(x => x)
                .Must(x => x.SenderAccountNumber != x.ReceiverAccountNumber)
                .WithMessage("Sender and Receiver account cannot be the same");


            
        }
    }
}
