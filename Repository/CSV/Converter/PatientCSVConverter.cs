using Bolnica.Model;
using Bolnica.Model.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Repository.CSV.Converter
{
    public class PatientCSVConverter : ICSVConverter<Patient>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        private readonly CultureInfo _cultureProvider;

        public PatientCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
            _cultureProvider = CultureInfo.InvariantCulture;
        }

        public string ConvertEntityToCSVFormat(Patient patient)
           => string.Join(_delimiter,
               patient.Id,
               patient.Punished,
               patient.Violated,
               patient.OneTimePatient,
               patient.FirstName,
               patient.LastName,
               patient.Email.Value,
               patient.Username,
               patient.Password.Value,
               patient.PhoneNumber.Value,
               patient.Address,
               patient.Gender,
               patient.BirthDate.ToString(_datetimeFormat));

        public Patient ConvertCSVFormatToEntity(string patientCSVFormat)
        {
            string[] tokens = patientCSVFormat.Split(_delimiter.ToCharArray());
            return new Patient(
                long.Parse(tokens[0]),
                bool.Parse(tokens[1]),
                bool.Parse(tokens[2]),
                bool.Parse(tokens[3]),
                tokens[4],
                tokens[5],
                new Email(tokens[6]),
                tokens[7],
                new Password(tokens[8]),
                new PhoneNumber(int.Parse(tokens[9])),
                tokens[10],
                tokens[11],
                DateTime.ParseExact(tokens[12], _datetimeFormat, _cultureProvider));
        }
    }
}
