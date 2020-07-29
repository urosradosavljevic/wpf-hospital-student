using Bolnica.Exception;
using System.Text.RegularExpressions;

namespace Bolnica.Model.Util
{
    public class BloodType
    {
        private const string ERROR_MESSAGE = "Invalid Blood Type. Acceptable blood types are A+, A-, B+, B-,O+ ,O- ,AB+ ,AB- ";

        private string _value;

        public BloodType(string bloodType)
        {
            if (IsBloodType(bloodType))
                _value = bloodType;
            else
                ThrowException();
        }

        public string Value
        {
            get => _value;
            set
            {
                if (IsBloodType(value))
                    _value = value;
                else
                    ThrowException();
            }
        }

        private bool IsBloodType(string bloodType)
            => bloodType == "A+" || 
            bloodType == "A-" || 
            bloodType == "B+" || 
            bloodType == "B-" || 
            bloodType == "0+" || 
            bloodType == "0-" || 
            bloodType == "AB+" || 
            bloodType == "AB-" 
            ? true : false;

        private void ThrowException()
            => throw new ValidationException(ERROR_MESSAGE);

    }
}
