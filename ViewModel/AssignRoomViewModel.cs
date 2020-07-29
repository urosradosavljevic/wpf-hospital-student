
using Bolnica.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class AssignRoomViewModel : BindableBase
    {
        public string HelpText { get; set; }
        MainWindowViewModel mainWindowDataContext;
        private App app;

        private string errorText;

        private Appointement selectedApointement;
        private Room selectedRoom;

        // Apointements
        public ObservableCollection<Appointement> appointements;
        public ObservableCollection<Appointement> FilteredApointements { get; set; }
        // Rooms 
        public ObservableCollection<Room> RoomsOptions { get; set; }

        // Commands
        public MyICommand SubmitAssignCommand { get; set; }

        public AssignRoomViewModel()
        {
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            HelpText = "You can see the list of scheduled examinations for the chosen parameters, click on button 'See the List' to see it.";

            app = Application.Current as App;

            SubmitAssignCommand = new MyICommand(OnSubmit, CanSubmit);

            RoomsOptions = new ObservableCollection<Room>();

            AddApointements();
            AddRoomsOptions();
           

        }

        #region Getters & Setters
        public string ErrorText
        {
            get
            {
                return errorText;
            }
            set
            {
                errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                if (selectedRoom != value)
                {
                    selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                    SubmitAssignCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public ObservableCollection<Appointement> Appointements
        {
            get { return appointements; }
            set
            {
                if (appointements != value)
                {
                    appointements = value;
                    OnPropertyChanged("Appointements");
                    SubmitAssignCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public Appointement SelectedApointement
        {
            get { return selectedApointement; }
            set
            {
                if (selectedApointement != value)
                {
                    selectedApointement = value;
                    OnPropertyChanged("SelectedApointement");
                    SubmitAssignCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Handlers
        public void OnSubmit()
        {
            int idx = FilteredApointements.IndexOf(SelectedApointement);

            if(idx >= 0)
            {
                FilteredApointements[idx].Room = SelectedRoom;
                var ap = FilteredApointements[idx];
                app.AppointementController.Update(ap);
            }

            AddApointements();

            OnPropertyChanged("FilteredApointements");
            SelectedApointement = null;
            SelectedRoom = null;
        }
        #endregion

        #region Data

        public void FilterApointements()
        {
            FilteredApointements = new ObservableCollection<Appointement>(from apointement in FilteredApointements where apointement.Room is null select apointement);
            OnPropertyChanged("FilteredApointements");
        }
        
        public void AddApointements()
        {
            Appointements = new ObservableCollection<Appointement>(app.AppointementController.GetAppointementsWithoutRoom());

            FilteredApointements = Appointements;
        }
        
        public void AddRoomsOptions()
        {
            var rooms = app.RoomController.GetAll();
            RoomsOptions = new ObservableCollection<Room>(rooms);
        }
        
        #endregion

        #region Validator Implementation
        private bool CanSubmit()
        {
            ErrorText = "";
            if (SelectedApointement is null)
            {
                ErrorText = "Select apointement to continue.";
                return false;
            }
            if (SelectedRoom is null)
            {
                ErrorText = "Select room to continue.";
                return false;
            }
            return true;
        }
        #endregion
    }
}