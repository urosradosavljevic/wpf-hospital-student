using Bolnica.Model;
using System;
using System.Globalization;

namespace Bolnica.Repository.CSV.Converter
{
    public class FeedbackCSVConverter : ICSVConverter<Feedback>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;
        private readonly CultureInfo _cultureProvider;
        public FeedbackCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
            _cultureProvider = CultureInfo.InvariantCulture;
        }

        public string ConvertEntityToCSVFormat(Feedback feedback)
           => string.Join(_delimiter,
               feedback.Id,
               feedback.Date.ToString(_datetimeFormat),
               feedback.Text
               );

        public Feedback ConvertCSVFormatToEntity(string feedbackCSVFormat)
        {
            string[] tokens = feedbackCSVFormat.Split(_delimiter.ToCharArray());
            return new Feedback(
                long.Parse(tokens[0]),
                DateTime.ParseExact(tokens[1], _datetimeFormat, _cultureProvider),
                tokens[2]
                );
        }
    }
}
