using Bolnica.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ScheduleApointementPeriodDoctorViewModel : BindableBase
    {
        private readonly App _app;
        public string HelpText { get; set; }
        MainWindowViewModel mainWindowDataContext;

        public string doctorNameText;
        public string errorText;

        private DateTime selectedDate;

        // Commands
        public MyICommand GoToSelectTermCommand { get; set; }
        public MyICommand DoctorNameChangedCommand { get; set; }

        // Doctor
        public Doctor selectedDoctor;
        public ObservableCollection<Doctor> DoctorsOptionsList { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Doctor> FilteredDoctorsOptionsList { get; set; } = new ObservableCollection<Doctor>();

        public ScheduleApointementPeriodDoctorViewModel()
        {
            _app = Application.Current as App;
            GoToSelectTermCommand = new MyICommand(OnGoToSelectTermCommand, CanContinue);
            DoctorNameChangedCommand = new MyICommand(OnDoctorNameChanged);
                
            SelectedDate = DateTime.Today;

            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            HelpText = "Schedule Apointement With Account Help text";

            AddDoctorOptions();

            FilteredDoctorsOptionsList = DoctorsOptionsList;
        }

        #region Getters & Setters

        public Doctor SelectedDoctor{
            get 
            {
                return selectedDoctor;
            } 
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
            } 
        }
        public string ErrorText
        {
            get 
            {
                return errorText;
            } 
            set
            {
                errorText = value;
                OnPropertyChanged("DataGridErrorText");
            } 
        }
        public DateTime SelectedDate {
            get 
            {
                return selectedDate;
            } 
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
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
        public void OnGoToSelectTermCommand()   
        {
            _app.TempAppointement.Doctor = SelectedDoctor;
            _app.TempAppointement.Date = SelectedDate;
            
            mainWindowDataContext.OnNav("schedule_apointement_term");
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

    }
}
