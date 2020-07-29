using Bolnica.Model;
using Bolnica.Model.Util;
using Bolnica.Validation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ApointementWithoutAccStartViewModel : BindableBase, IDataErrorInfo
    {
        private readonly App _app;
        public string HelpText { get; set; }
        ApointementWithoutStartValidator _apointementWithoutStartValidator;

        private string errorText;

        private string patientFirstName;
        private string patientEmail;
        private string patientLastName;
        private string patientGender;
        private string patientPhoneNumber;
        private string patientAddress;
        private DateTime patientDateOfBirth;

        MainWindowViewModel mainWindowDataContext;
        // Commands
        public MyICommand<string> SelectGenderCommand { get; set; }
        public MyICommand GoToSelectTermCommand { get; set; }


        public ApointementWithoutAccStartViewModel()
        {
            _app = Application.Current as App;
            HelpText = "Schedule Apointement Without Help text";
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            _apointementWithoutStartValidator = new ApointementWithoutStartValidator();

            GoToSelectTermCommand = new MyICommand(OnGoToSelectTermCommand, CanContinue);
            SelectGenderCommand = new MyICommand<string>(OnSelectGenderCommand);
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
        public string PatientEmail
        {
            get 
            {
                return patientEmail;
            } 
            set
            {
                patientEmail = value;
                OnPropertyChanged("PatientEmail");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
            } 
        }
        public string PatientFirstName
        {
            get 
            {
                return patientFirstName;
            } 
            set
            {
                patientFirstName = value;
                OnPropertyChanged("PatientFirstName");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
            } 
        }
        public string PatientLastName
        {
            get 
            {
                return patientLastName;
            } 
            set
            {
                patientLastName = value;
                OnPropertyChanged("PatientLastName");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
            } 
        }
        public string PatientPhoneNumber
        {
            get 
            {
                return patientPhoneNumber;
            } 
            set
            {
                patientPhoneNumber = value;
                OnPropertyChanged("PatientPhoneNumber");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
            } 
        }
        public string PatientGender
        {
            get 
            {
                return patientGender;
            } 
            set
            {
                patientGender = value;
                OnPropertyChanged("PatientGender");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
            } 
        }
        public string PatientAddress
        {
            get 
            {
                return patientAddress;
            } 
            set
            {
                patientAddress = value;
                OnPropertyChanged("PatientAddress");
                GoToSelectTermCommand.RaiseCanExecuteChanged();
            } 
        }
        public DateTime PatientDateOfBirth
        {
            get 
            {
                return patientDateOfBirth;
            } 
            set
            {
                patientDateOfBirth = value;
                OnPropertyChanged("PatientDateOfBirth");
            } 
        }

        #endregion

        #region Handlers
        private void OnSelectGenderCommand(string gender)
        {
            PatientGender = gender;
        }

        public void OnGoToSelectTermCommand()
        {
            // Temp patient with generic data            
            var patient = new Patient(false,false,true,PatientFirstName,PatientLastName,new Email(PatientEmail),"", new Password("generic11"), new PhoneNumber(int.Parse(PatientPhoneNumber)), PatientAddress, PatientGender, PatientDateOfBirth);
            _app.TempAppointement = new Appointement() { Patient = patient };
            
            mainWindowDataContext.BackNavRoute = "apointement_without_account";
            mainWindowDataContext.BackButtonEnabled = true;

            mainWindowDataContext.OnNav("schedule_apointement_period_doctor");
        }

        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            bool valid = _apointementWithoutStartValidator.Validate(this).IsValid;
            ErrorText = "";
            if (PatientGender == null)
            {
                ErrorText = "Select gender to continue.";
                return false;
            }
            return valid;
        }
        public string Error
        {
            get
            {
                if (_apointementWithoutStartValidator != null)
                {
                    var results = _apointementWithoutStartValidator.Validate(this);
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
                var firstOrDefault = _apointementWithoutStartValidator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _apointementWithoutStartValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
        #endregion

    }
}
