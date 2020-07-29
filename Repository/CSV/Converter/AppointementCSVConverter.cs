using Bolnica.Model;
using Bolnica.Model.Util;
using System;
using System.Globalization;

namespace Bolnica.Repository.CSV.Converter
{
    public class AppointementCSVConverter : ICSVConverter<Appointement>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        private readonly CultureInfo _cultureProvider;

        public AppointementCSVConverter(string delimiter,string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
            _cultureProvider = CultureInfo.InvariantCulture;
        }

        public string ConvertEntityToCSVFormat(Appointement appointement)
           => string.Join(_delimiter,
               appointement.Id,
               appointement.Doctor.Id,
               appointement.Patient.Id,
               appointement.Date.ToString(_datetimeFormat),
               appointement.Term.Value,
               appointement.Room.Id,
               appointement.Intervention,
               appointement.Emergency.ToString());

        public Appointement ConvertCSVFormatToEntity(string appointementCSVFormat)
        {
            string[] tokens = appointementCSVFormat.Split(_delimiter.ToCharArray());
            return new Appointement(
                long.Parse(tokens[0]),
                new Doctor(long.Parse(tokens[1])),
                new Patient(long.Parse(tokens[2])),
                DateTime.ParseExact(tokens[3], _datetimeFormat, _cultureProvider),
                new Term(tokens[4]),
                new Room(long.Parse(tokens[5])),
                tokens[6],
                bool.Parse(tokens[7]));
        }


    }
}
