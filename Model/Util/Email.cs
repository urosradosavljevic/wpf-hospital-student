using Bolnica.Exception;
using System.Text.RegularExpressions;

namespace Bolnica.Model.Util
{
    public class Email
    {
        private readonly Regex _regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        private const string ERROR_MESSAGE = "Invalid email. Acceptable format is john@mail.com";

        private string _value;

        public Email(string email)
        {
            if (IsEmailFormat(email))
                _value = email;
            else
                ThrowException();
        }

        public string Value
        {
            get => _value;
            set
            {
                if (IsEmailFormat(value))
                    _value = value;
                else
                    ThrowException();
            }
        }

        private bool IsEmailFormat(string accountNumber)
            => _regex.Match(accountNumber).Success;

        private void ThrowException()
            => throw new ValidationException(ERROR_MESSAGE);

    }
}
