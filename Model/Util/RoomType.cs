using Bolnica.Exception;
using System.Text.RegularExpressions;

namespace Bolnica.Model.Util
{
    public class RoomType
    {
        private const string ERROR_MESSAGE = "Invalid room type. Room types have to be SURGERY, EXAMINATION";

        private string _value;

        public RoomType(string roomType)
        {
            if (IsRoomType(roomType))
                _value = roomType;
            else
                ThrowException();
        }

        public string Value
        {
            get => _value;
            set
            {
                if (IsRoomType(value))
                    _value = value;
                else
                    ThrowException();
            }
        }

        private bool IsRoomType(string bloodType)
            => bloodType == "SURGERY" ||
            bloodType == "EXAMINATION" 
            ? true : false;

        private void ThrowException()
            => throw new ValidationException(ERROR_MESSAGE);

    }
}
