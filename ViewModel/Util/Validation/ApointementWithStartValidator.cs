using System;
using Bolnica.ViewModel;
using FluentValidation;

namespace Bolnica.Validation
{
    class ApointementWithStartValidator : AbstractValidator<ApointementWithAccStartViewModel >
    {
        public ApointementWithStartValidator()
        {
            RuleFor(start => start.PatientEmailText)
                .NotEmpty().WithMessage("Required field.")
                .EmailAddress().WithMessage("Email is not Valid.");
        }
    }
}
