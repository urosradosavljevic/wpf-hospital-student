using Bolnica.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ScheduledApointementDoctorChangeViewModel : BindableBase
    {
        private readonly App _app;
        public string HelpText { get; set; }
        MainWindowViewModel mainWindowDataContext;

        private string errorText;
        private string doctorNameText;

        // Commands
        public MyICommand GoToNextPageCommand { get; set; }
        public MyICommand DoctorNameChangedCommand { get; set; }

        // Doctor
        public Doctor selectedDoctor;
        public ObservableCollection<Doctor> DoctorsOptionsList { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Doctor> FilteredDoctorsOptionsList { get; set; } = new ObservableCollection<Doctor>();
        // Apointemet
        public Appointement ApointementToChange { get; set; }

        public ScheduledApointementDoctorChangeViewModel()
        {
            _app = Application.Current as App;
            HelpText = "Schedule Apointement Term Help text";
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;

            // ApointementToChange = mainWindowDataContext.CashedApointement;

            GoToNextPageCommand = new MyICommand(OnGoToNextPage, CanContinue);
            DoctorNameChangedCommand = new MyICommand(OnDoctorNameChanged);

            //AddDoctorOptions();
            FilteredDoctorsOptionsList = DoctorsOptionsList;
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
        public Doctor SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
                GoToNextPageCommand.RaiseCanExecuteChanged();
            }
        }
        public string DoctorNameText
        {
            get
            {
                return doctorNameText;
            }
            set
            {
                doctorNameText = value;
                OnPropertyChanged("DoctorNameText");
            }
        }

        #endregion

        #region Data
        public void AddDoctorOptions()
        {
            DoctorsOptionsList = new ObservableCollection<Doctor>(_app.DoctorController.GetAll());
        }
        #endregion

        #region Handlers
        public void OnGoToNextPage()
        {
            _app.TempAppointement.Doctor = SelectedDoctor;
            _app.AppointementController.Update(_app.TempAppointement);
            mainWindowDataContext.OnNav("scheduled_apointements_display");
        }
        private void OnDoctorNameChanged()
        {
            FilteredDoctorsOptionsList = new ObservableCollection<Doctor>(from doctor in DoctorsOptionsList where doctor.FirstName.Contains(DoctorNameText) select doctor);
            OnPropertyChanged("FilteredDoctorsOptionsList");
        }
        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            ErrorText = "";
            if (SelectedDoctor is null)
            {
                ErrorText = "Select doctor to continue.";
                return false;
            }
            return true;
        }
        #endregion

        public void SetApointement()
        {
            // ApointementToChange = mainWindowDataContext.CashedApointement;
        }
    }
}
