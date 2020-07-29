using Bolnica.Model.Util;
using Bolnica.Repository.Abstract;
using System.ComponentModel;

namespace Bolnica.Model
{
    public class Room: INotifyPropertyChanged,IIdentifiable<long>
    {
        private long id;
        private int roomNumber;
        private RoomType roomType;

        public Room(long _id)
        {
            Id = _id;
        }

        public Room(long _id,int _roomNumber,RoomType _roomType)
        {
            Id = _id;
            RoomNumber = _roomNumber;
            RoomType = _roomType;
        }
        
        public Room(int _roomNumber,RoomType _roomType)
        {
            RoomNumber = _roomNumber;
            RoomType = _roomType;
        }

        #region Getters and setters
        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaiseProperyChanged("Id");
            }
        }        
        public int RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                roomNumber = value;
                RaiseProperyChanged("RoomNumber");
            }
        }        
        public RoomType RoomType
        {
            get
            {
                return roomType;
            }
            set
            {
                roomType = value;
                RaiseProperyChanged("RoomType");
            }
        }
        public long GetId() => Id;

        public void SetId(long id) => Id = id;
        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseProperyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
