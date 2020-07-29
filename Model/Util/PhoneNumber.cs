using Bolnica.Exception;

namespace Bolnica.Model.Util
{
    public class PhoneNumber
    {
        private const string ERROR_MESSAGE = "Invalid phone number. The number must be 9 or 10 digits long";

        private int _value;

        public PhoneNumber(int phoneNumber)
        {
            if (IsPhoneNumberFormat(phoneNumber))
                _value = phoneNumber;
            else
                ThrowException();
        }

        public int Value
        {
            get => _value;
            set
            {
                if (IsPhoneNumberFormat(value))
                    _value = value;
                else
                    ThrowException();
            }
        }

        private bool IsPhoneNumberFormat(int phoneNumber)
            => phoneNumber.ToString().Length == 9 || phoneNumber.ToString().Length == 10 ? true : false ;

        private void ThrowException()
            => throw new ValidationException(ERROR_MESSAGE);

    }
}
