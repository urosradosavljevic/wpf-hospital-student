using Bolnica.Exception;
using System.Text.RegularExpressions;

namespace Bolnica.Model.Util
{
    public class Term
    {
        private const string ERROR_MESSAGE = "Invalid Term. Acceptable term formats 00-30:01:30";

        private string _value;

        public Term(string term)
        {
            if (IsValidTerm(term))
                _value = term;
            else
                ThrowException();
        }

        public string Value
        {
            get => _value;
            set
            {
                if (IsValidTerm(value))
                    _value = value;
                else
                    ThrowException();
            }
        }

        private bool IsValidTerm(string term)
            => term == "00:00-01:30" ||
            term == "01:30-03:00" ||
            term == "03:00-04:30" ||
            term == "04:30-06:00" ||
            term == "06:00-07:30" ||
            term == "07:30-09:00" ||
            term == "09:00-10:30" ||
            term == "10:30-12:00" ||
            term == "12:00-13:30" ||
            term == "13:30-15:00" ||
            term == "15:00-16:30" ||
            term == "16:30-18:00" ||
            term == "18:00-19:30" ||
            term == "21:00-22:30" ||
            term == "22:30-00:00"
            ? true : false;

        private void ThrowException()
            => throw new ValidationException(ERROR_MESSAGE);

    }
}
