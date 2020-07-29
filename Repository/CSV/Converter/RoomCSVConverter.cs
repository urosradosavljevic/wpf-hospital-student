using Bolnica.Model;
using Bolnica.Model.Util;

namespace Bolnica.Repository.CSV.Converter
{
    public class RoomCSVConverter : ICSVConverter<Room>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;

        public RoomCSVConverter(string delimiter,string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }

        public string ConvertEntityToCSVFormat(Room room)
           => string.Join(_delimiter,
               room.Id,
               room.RoomNumber,
               room.RoomType.Value
               );

        public Room ConvertCSVFormatToEntity(string acountCSVFormat)
        {
            string[] tokens = acountCSVFormat.Split(_delimiter.ToCharArray());
            return new Room(
                long.Parse(tokens[0]),
                int.Parse(tokens[1]),
                new RoomType(tokens[2])
                );
        }

    }
}
