using System;
using Bolnica.ViewModel;
using FluentValidation;

namespace Bolnica.Validation
{
    class ScheduledApointementsStartValidator : AbstractValidator<ScheduledApointementsStartViewModel>
    {
        public ScheduledApointementsStartValidator()
        {
            RuleFor(start => start.PatientEmailText)
                .EmailAddress().WithMessage("Email is not Valid.");
        }

        private bool BeNumber(string roomNumberText) {
            int number;
            return Int32.TryParse(roomNumberText,out number);
        }

        private bool LessThan(string roomNumberText) {
            int number;
            Int32.TryParse(roomNumberText,out number);
            return number < 320 ? true : false;
        }

        private bool GreaterThan(string roomNumberText) {
            int number;
            Int32.TryParse(roomNumberText,out number);
            return number >= 0 ? true : false;
        }
    }
}
