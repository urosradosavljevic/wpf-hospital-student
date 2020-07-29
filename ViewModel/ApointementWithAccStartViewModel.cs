using Bolnica.Controller;
using Bolnica.Controller.Abstract;
using Bolnica.Model;
using Bolnica.Validation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ApointementWithAccStartViewModel : BindableBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly App _app;
        MainWindowViewModel mainWindowDataContext;
        ApointementWithStartValidator _apointementWithStartValidator;
        public string HelpText { get; set; }
        public string doctorNameText;
        public string dataGridErrorText;

        private DateTime appointementDate;

        private readonly IAppointementController _appointementController;
        private readonly IController<Doctor, long> _doctorController;
        private readonly IPatientController _patientController;
        // Commands
        public MyICommand GoToSelectTermCommand { get; set; }
        public MyICommand PatientEmailChangedCommand { get; set; }
        public MyICommand DoctorNameChangedCommand { get; set; }

        // Patient
        public string patientEmailText;

        // Doctor
        public Doctor selectedDoctor;
        public ObservableCollection<Doctor> DoctorsOptionsList { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Doctor> FilteredDoctorsOptionsList { get; set; } = new ObservableCollection<Doctor>();

        public ApointementWithAccStartViewModel()
        {
            _apointementWithStartValidator = new ApointementWithStartValidator();

            GoToSelectTermCommand = new MyICommand(OnGoToSelectTermCommand, CanContinue);
            DoctorNameChangedCommand = new MyICommand(OnDoctorNameChanged);
            PatientEmailChangedCommand = new MyICommand(OnPatientEmailChanged);

            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            HelpText = "Schedule Apointement Help text";

            AppointementDate = DateTime.Today;

            _app = Application.Current as App;
            _appointementController = _app.AppointementController;

            _doctorController = _app.DoctorController;
            _patientController = _app.PatientController;
            _patientController = _app.PatientController;

            DoctorsOptionsList = new ObservableCollection<Doctor>(_doctorController.GetAll().ToList());

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
        public string PatientEmailText{
            get 
            {
                return patientEmailText;
            } 
            set
            {
                patientEmailText = value;
                OnPropertyChanged("PatientEmailText");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
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
        public DateTime AppointementDate
        {
            get 
            {
                return appointementDate;
            } 
            set
            {
                appointementDate = value;
                OnPropertyChanged("AppointementDate");
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
        #endregion

        #region Handlers

        public void OnGoToSelectTermCommand()
        {
            _app.TempAppointement = new Appointement() { Doctor = SelectedDoctor, Patient = _patientController.GetPatientByEmail(PatientEmailText)};
            _app.TempDateTime = AppointementDate;

            mainWindowDataContext.OnNav("schedule_apointement_term");
        }

        private void OnDoctorNameChanged()
        {
            FilteredDoctorsOptionsList = new ObservableCollection<Doctor>(from doctor in DoctorsOptionsList where doctor.FirstName.Contains(DoctorNameText) select doctor);
            OnPropertyChanged("FilteredDoctorsOptionsList");
        }
        private void OnPatientEmailChanged()
        {
            DataGridErrorText = "";
            var o = _patientController.GetPatientByEmail(PatientEmailText);
            if (o is null)
            {
                DataGridErrorText = "Patient with that email doesn't exist.";
            }
        }
        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            //_appointementController.GetAppointementsForSpecificPeriod(periodFrom,periodTo);
            Console.WriteLine(AppointementDate.Date);
            DataGridErrorText = "";
            bool valid = _apointementWithStartValidator.Validate(this).IsValid;
            if (SelectedDoctor is null)
            {
                DataGridErrorText = "Select doctor to continue.";
                return false;
            }
            return valid;
        }
        public string Error
        {
            get
            {
                if (_apointementWithStartValidator != null)
                {
                    var results = _apointementWithStartValidator.Validate(this);
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
                var firstOrDefault = _apointementWithStartValidator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _apointementWithStartValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
        #endregion

    }
}
