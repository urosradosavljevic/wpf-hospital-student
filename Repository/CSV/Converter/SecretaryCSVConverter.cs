using Bolnica.Model;
using Bolnica.Model.Util;
using System;
using System.Globalization;

namespace Bolnica.Repository.CSV.Converter
{
    public class SecretaryCSVConverter : ICSVConverter<Secretary>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        private readonly CultureInfo _cultureProvider;

        public SecretaryCSVConverter(string delimiter,string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
            _cultureProvider = CultureInfo.InvariantCulture;
        }

        public string ConvertEntityToCSVFormat(Secretary secretary)
           => string.Join(_delimiter,
               secretary.Id,
               secretary.FirstName,
               secretary.LastName,
               secretary.Email.Value,
               secretary.Username,
               secretary.Password.Value,
               secretary.PhoneNumber.Value,
               secretary.Address,
               secretary.Gender,
               secretary.BirthDate.ToString(_datetimeFormat));

        public Secretary ConvertCSVFormatToEntity(string acountCSVFormat)
        {
            string[] tokens = acountCSVFormat.Split(_delimiter.ToCharArray());
            return new Secretary(
                long.Parse(tokens[0]),
                tokens[1],
                tokens[2],
                new Email(tokens[3]),
                tokens[4],
                new Password(tokens[5]),
                new PhoneNumber(int.Parse(tokens[6])),
                tokens[7], tokens[8],
                DateTime.ParseExact(tokens[9], _datetimeFormat, _cultureProvider));
        }
    }
}
