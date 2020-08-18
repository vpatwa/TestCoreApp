using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TestCoreApp.Domain.Contracts.Request;

namespace TestCoreApp.Application.Contracts.UpdateContract
{
    public class UpdateContractCommandValidator : AbstractValidator<UpdateContractCommand>
    {
        public UpdateContractCommandValidator()
        {
            RuleFor(v => v.CustomerName)
               .NotEmpty().WithMessage("Customer Name is required");

            RuleFor(v => v.CustomerAddress)
               .NotEmpty().WithMessage("Customer Address is required");

            RuleFor(v => v.SaleDate)
               .NotEmpty().WithMessage("Sale Date is required");

            RuleFor(v => v.CustomerDOB)
               .NotEmpty().WithMessage("Date of Birth is required");

            RuleFor(v => v.Gender)
               .NotEmpty().WithMessage("Customer Name is required");
        }
    }
}
