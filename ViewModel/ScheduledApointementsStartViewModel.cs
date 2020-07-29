using Bolnica.Model;
using Bolnica.Validation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using Bolnica.Controller.Abstract;

namespace Bolnica.ViewModel
{
    class ScheduledApointementsStartViewModel : BindableBase, IDataErrorInfo
    {
        private readonly App _app;
        private readonly IPatientController _patientController;

        MainWindowViewModel mainWindowDataContext;
        ScheduledApointementsStartValidator scheduledApointementsStartValidator;
        public string HelpText { get; set; }

        // Commands
        public MyICommand NextPageCommand { get; set; }
        public MyICommand PatientEmailChangedCommand { get; set; }
        
        private string errorText;
        private string nextButtonText;
        private bool applyDateFilter;
        private bool allFlag;
        private DateTime appointementDate;

        private ObservableCollection<Appointement> filteredAppointements;
        // Doctors
        public ObservableCollection<Doctor> DoctorsOptions { get; set; }
        public Doctor selectedDoctor;
        //Patient
        public string patientEmailText;
        //Room
        public ObservableCollection<Room> RoomOptions { get; set; }
        public Room selectedRoom;

        public ScheduledApointementsStartViewModel()
        {
            _app = Application.Current as App;
            _patientController = _app.PatientController;

            AppointementDate = DateTime.Now;
            AllFlag = true;

            nextButtonText = "ShowAll";

            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            HelpText = "After choosing parameters you can see the list of scheduled examinations or calendar of scheduled examinations, click on button 'See the List' or 'See the Calendar' to see it. ";
            scheduledApointementsStartValidator = new ScheduledApointementsStartValidator();

            NextPageCommand = new MyICommand(OnNextPage, CanContinue);
            PatientEmailChangedCommand = new MyICommand(OnPatientEmailChanged);

            DoctorsOptions = new ObservableCollection<Doctor>();


            AddDoctorsOptions();
            AddAppointements();
            AddRoomsOptions();
        }

        #region Getters & Setters
        public string ErrorText
        {
            get { return errorText; }
            set
            {
                if (errorText != value)
                {
                    errorText = value;
                    OnPropertyChanged("ErrorText");
                }
            }
        }
        public bool ApplyDateFilter
        {
            get { return applyDateFilter; }
            set
            {
                if (applyDateFilter != value)
                {
                    applyDateFilter = value;
                    ApplyDateChanged();
                }
            }
        }
        public bool AllFlag
        {
            get { return allFlag; }
            set
            {
                if (allFlag != value)
                {
                    allFlag = value;
                    OnPropertyChanged("AllFlag");
                }
            }
        }
        public string NextButtonText
        {
            get { return nextButtonText; }
            set
            {
                if (nextButtonText != value)
                {
                    nextButtonText = value;
                    OnPropertyChanged("NextButtonText");
                }
            }
        }
        public DateTime AppointementDate
        {
            get { return appointementDate; }
            set
            {
                if (appointementDate != value)
                {
                    appointementDate = value;
                    OnPropertyChanged("ErrorText");
                }
            }
        }
        public string PatientEmailText
        {
            get
            {
                return patientEmailText;
            }
            set
            {
                patientEmailText = value;
                OnPropertyChanged("PatientEmailText");
                NextPageCommand.RaiseCanExecuteChanged();
            }
        }
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                if (selectedDoctor != value)
                {
                    selectedDoctor = value;
                    OnPropertyChanged("SelectedDoctor");
                    DoctorChangedHandler();
                }
            }
        }
        public ObservableCollection<Appointement> FilteredAppointements
        {
            get { return filteredAppointements; }
            set
            {
                if (filteredAppointements != value)
                {
                    filteredAppointements = value;
                    OnPropertyChanged("FilteredAppointements");
                    CheckAppointementsLenght();
                    
                }
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
                    RoomChangedHandler();
                }
            }
        }
        #endregion

        #region Handlers
        public void OnNextPage()
        {
            _app.TempScheduled = FilteredAppointements;
            mainWindowDataContext.BackNavRoute = "scheduled_apointements";
            mainWindowDataContext.BackButtonEnabled = true;

            mainWindowDataContext.OnNav("scheduled_apointements_display");
        }
        public void ApplyDateChanged()
        {
            AllFlag = true;
            if (applyDateFilter)
            {
                FilteredAppointements = new ObservableCollection<Appointement>(from a in FilteredAppointements where a.Date == AppointementDate select a);
            }
            OnPropertyChanged("ApplyDateFilter");
        }
        private void OnPatientEmailChanged()
        {
            ErrorText = ""; 
            AllFlag = false;
            var o = _patientController.GetPatientByEmail(PatientEmailText);
            if (o is null)
            {
                ErrorText = "Patient with that email doesn't exist.";
            }
            AddAppointements();
            FilteredAppointements = new ObservableCollection<Appointement>(from a in FilteredAppointements where a.Patient.Email.Value == PatientEmailText select a);
            AllFlag = true;
            NextPageCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region Data

        public void FilterAppointementsByDoctor()
        {
            FilteredAppointements = new ObservableCollection<Appointement>(from a in FilteredAppointements where a.Doctor.Id == SelectedDoctor.Id select a);
        }
        public void FilterAppointementsByRoom()
        {                
            FilteredAppointements = new ObservableCollection<Appointement>(from a in FilteredAppointements where a.Room.Id == SelectedRoom.Id select a);
        }
        public void AddAppointements()
        {                
            var aapps = _app.AppointementController.GetAll();
            FilteredAppointements = new ObservableCollection<Appointement>(aapps);
        }
        public void AddDoctorsOptions()
        {
            var doctors = _app.DoctorController.GetAll();
            DoctorsOptions = new ObservableCollection<Doctor>(doctors);
        }
        public void AddRoomsOptions()
        {
            var rooms = _app.RoomController.GetAll();
            RoomOptions = new ObservableCollection<Room>(rooms);
        }

        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            bool valid = scheduledApointementsStartValidator.Validate(this).IsValid;
            return valid;
        }
        private void CheckAppointementsLenght()
        {
            ErrorText = "";
            if (FilteredAppointements.Count == 0)
            {
                ErrorText = "There are no appointements with selected filters.";
            }
            NextPageCommand.RaiseCanExecuteChanged();
        }
        private void DoctorChangedHandler()
        {
            FilterAppointementsByDoctor();
            AllFlag = true;
            NextPageCommand.RaiseCanExecuteChanged();
        }
        private void RoomChangedHandler()
        {
            OnPropertyChanged("SelectedRoom");
            AddAppointements();
            FilterAppointementsByRoom();
            AllFlag = true;
            NextPageCommand.RaiseCanExecuteChanged();
        }
        public string Error
        {
            get
            {
                if (scheduledApointementsStartValidator != null)
                {
                    var results = scheduledApointementsStartValidator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = scheduledApointementsStartValidator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return scheduledApointementsStartValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
        #endregion

    }
}