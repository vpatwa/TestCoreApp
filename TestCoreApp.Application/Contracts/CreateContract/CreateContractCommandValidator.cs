using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCoreApp.Domain.Contracts.Request;

namespace TestCoreApp.Application.Contracts.CreateContract
{
    public class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
    {
        public CreateContractCommandValidator()
        {
            RuleFor(v => v.CustomerName)
               .NotEmpty().WithMessage("Customer Name is required.");

            RuleFor(v => v.CustomerAddress)
               .NotEmpty().WithMessage("Customer Address is required.");

            RuleFor(v => v.SaleDate)
               .NotEmpty().WithMessage("Sale Date is required.");

            RuleFor(v => v.CustomerDOB)
               .NotEmpty().WithMessage("Date of Birth is required.");

            RuleFor(v => v.Gender)
               .Must(IsValidGender).WithMessage("Invalid Customer Gender.")
               .NotEmpty().WithMessage("Customer Name is required.");
        }

        private bool IsValidGender(string Gender)
        {
            List<string> genderValues = new List<string> { "M", "F" };

            return genderValues.Any(a => a == Gender);
        }
    }
}
