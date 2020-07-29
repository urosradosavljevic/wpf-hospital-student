using Bolnica.Exception;
using System.Text.RegularExpressions;

namespace Bolnica.Model.Util
{
    public class Rate
    {
        private const string ERROR_MESSAGE = "Invalid rating. The number must be between 0 and 10";

        private double _value;

        public Rate(double rate)
        {
            if (IsRateNumberFormat(rate))
                _value = rate;
            else
                ThrowException();
        }

        public double Value
        {
            get => _value;
            set
            {
                if (IsRateNumberFormat(value))
                    _value = value;
                else
                    ThrowException();
            }
        }

        private bool IsRateNumberFormat(double rate)
            => rate < 0 || rate > 10 ? false : true;

        private void ThrowException()
            => throw new ValidationException(ERROR_MESSAGE);

    }
}
