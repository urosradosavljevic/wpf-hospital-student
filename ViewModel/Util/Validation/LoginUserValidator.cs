using System;
using Bolnica.ViewModel;
using FluentValidation;

namespace Bolnica.Validation
{
    public class LoginUserValidator: AbstractValidator<LoginViewModel>
    {
        public LoginUserValidator()
        {
            RuleFor(user => user.EMText)
                .NotEmpty().WithMessage("Required field.")
                .EmailAddress().WithMessage("Email is not Valid.");
        }
    }
}
