using Bolnica.Exception;
using System.Text.RegularExpressions;

namespace Bolnica.Model.Util
{
    public class Password
    {
        private readonly Regex _regex = new Regex(@"^(?=.*\d).{5,12}$");
        private const string ERROR_MESSAGE = "Invalid password. Password must be between 5 and 12 chars long and include at least one numeric digit.";

        private string _value;

        public Password(string password)
        {
            if (IsPasswordFormat(password))
                _value = password;
            else
                ThrowException();
        }

        public string Value
        {
            get => _value;

            set
            {
                if (IsPasswordFormat(value))
                    _value = value;
                else
                    ThrowException();
            }
        }

        private bool IsPasswordFormat(string password)
            => _regex.Match(password).Success;

        private void ThrowException()
            => throw new ValidationException(ERROR_MESSAGE);

    }
}
