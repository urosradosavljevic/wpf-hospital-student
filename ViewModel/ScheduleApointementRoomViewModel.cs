using Bolnica.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ScheduleApointementRoomViewModel:BindableBase, INotifyPropertyChanged
    {
        private readonly App _app;
        MainWindowViewModel mainWindowDataContext;
        public string HelpText { get; set; }
        public string roomNumberSearch;
        public string dataGridErrorText;

        // Commands
        public MyICommand RoomSearchChangedCommand { get; set; }
        public MyICommand SubmitApointementCommand { get; set; }
        // Rooms
        public ObservableCollection<Room> RoomOptions { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<Room> FilteredRoomOptions { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<string> RoomSortingOptions { get; set; } = new ObservableCollection<string>();
        public Room selectedRoom;
        
        public ScheduleApointementRoomViewModel()
        {
            _app = Application.Current as App;
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            HelpText = "Schedule Apointement Submit Help text";
            RoomSearchChangedCommand = new MyICommand(OnRoomSearchChanged);
            SubmitApointementCommand = new MyICommand(OnSubmitApointement, CanContinue);

            AddRoomOptions();

            FilteredRoomOptions = RoomOptions;
        }

        #region Getters & Setters

        public Room SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
                SubmitApointementCommand.RaiseCanExecuteChanged();
            }
        }

        public string DataGridErrorText
        {
            get
            {
                return dataGridErrorText;
            }
            set
            {
                dataGridErrorText = value;
                OnPropertyChanged("DataGridErrorText");
            }
        }

        public string RoomNumberSearch
        {
            get
            {
                return roomNumberSearch;
            }
            set
            {
                roomNumberSearch = value;
                OnPropertyChanged("RoomNumberSearch");
            }
        }

        #endregion

        #region Data 
        
        public void AddRoomOptions()
        {
            RoomOptions = new ObservableCollection<Room>(_app.RoomController.GetAll());
        }

        
        #endregion

        #region Handlers

        private void OnRoomSearchChanged()
        {
            int roomNumberInt = 0;
            bool result = int.TryParse(RoomNumberSearch, out roomNumberInt);
            if (result)
            {
                FilteredRoomOptions = new ObservableCollection<Room>(from room in RoomOptions where room.RoomNumber == roomNumberInt select room);
            }
            if(RoomNumberSearch == "")
            {
                FilteredRoomOptions = RoomOptions;
            }
            
            OnPropertyChanged("FilteredRoomOptions");
        }
        public void OnSubmitApointement()
        {
            _app.TempAppointement.Room = SelectedRoom;
            _app.TempAppointement.Intervention = "EXAMINATION";
            mainWindowDataContext.OnNav("schedule_apointement_submit");
        }

        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            DataGridErrorText = "";
            if (SelectedRoom is null)
            {
                DataGridErrorText = "Select room to continue.";
                return false;
            }
            return true;
        }
        #endregion

    }
}
