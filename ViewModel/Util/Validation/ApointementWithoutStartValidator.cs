using System;
using Bolnica.ViewModel;
using FluentValidation;

namespace Bolnica.Validation
{
    class ApointementWithoutStartValidator : AbstractValidator<ApointementWithoutAccStartViewModel>
    {
        public ApointementWithoutStartValidator()
        {
            RuleFor(start => start.PatientEmail)
                .NotEmpty().WithMessage("Required field.")
                .EmailAddress().WithMessage("Email is not Valid.");
            RuleFor(start => start.PatientFirstName)
                .NotEmpty().WithMessage("Required field.");
            RuleFor(start => start.PatientLastName)
                .NotEmpty().WithMessage("Required field.");
            RuleFor(start => start.PatientPhoneNumber)
                .NotEmpty().WithMessage("Required field.");
            RuleFor(start => start.PatientAddress)
                .NotEmpty().WithMessage("Required field.");
        }
    }
}
