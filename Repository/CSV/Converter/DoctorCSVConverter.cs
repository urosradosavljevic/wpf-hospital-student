using Bolnica.Model;
using Bolnica.Model.Util;
using System;
using System.Globalization;

namespace Bolnica.Repository.CSV.Converter
{
    public class DoctorCSVConverter : ICSVConverter<Doctor>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        private readonly CultureInfo _cultureProvider;

        public DoctorCSVConverter(string delimiter,string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
            _cultureProvider = CultureInfo.InvariantCulture;
        }

        public string ConvertEntityToCSVFormat(Doctor doctor)
           => string.Join(_delimiter,
               doctor.Id,
               doctor.Rating.Value,
               doctor.FirstName,
               doctor.LastName,
               doctor.Email.Value,
               doctor.Username,
               doctor.Password.Value,
               doctor.PhoneNumber.Value,
               doctor.Address,
               doctor.Gender,
               doctor.BirthDate.ToString(_datetimeFormat));

        public Doctor ConvertCSVFormatToEntity(string acountCSVFormat)
        {
            string[] tokens = acountCSVFormat.Split(_delimiter.ToCharArray());
            return new Doctor(
                long.Parse(tokens[0]),
                new Rate(double.Parse(tokens[1])),
                tokens[2],
                tokens[3],
                new Email(tokens[4]),
                tokens[5],
                new Password(tokens[6]),
                new PhoneNumber(int.Parse(tokens[7])),
                tokens[8], tokens[9],
                DateTime.ParseExact(tokens[10], _datetimeFormat, _cultureProvider));
        }
    }
}
