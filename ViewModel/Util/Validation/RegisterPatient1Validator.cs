using System;
using Bolnica.ViewModel;
using FluentValidation;

namespace Bolnica.Validation
{
    class RegisterPatient1Validator: AbstractValidator<RegisterPatientView1Model>
    {
        public RegisterPatient1Validator()
        {
            RuleFor(user => user.FNText)
                .NotEmpty().WithMessage("Required field.");

            RuleFor(user => user.LNText)
                .NotEmpty().WithMessage("Required field.");

            RuleFor(user => user.UNText)
                .NotEmpty().WithMessage("Required field.");

            RuleFor(user => user.PNText)
                .NotEmpty().WithMessage("Required field.");

            RuleFor(user => user.ADRText)
                .NotEmpty().WithMessage("Required field.");

            RuleFor(user => user.EMText)
                .NotEmpty().WithMessage("Required field.")
                .EmailAddress().WithMessage("Email is not Valid.");
        }
    }
}
