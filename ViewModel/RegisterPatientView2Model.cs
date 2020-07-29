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
    class RegisterPatientView2Model : BindableBase
    {
        private readonly App _app;
        public string HelpText {get; set;}
        private string errorText;
        MainWindowViewModel mainWindowDataContext;
        // BloodType
        public ObservableCollection<BloodType> BloodTypeOptions { get; set; }
        public BloodType selectedBloodType;
        // Allergies
        public ObservableCollection<string> AllergiesOptions { get; set; }
        public string selectedAllergy;
        // Commands
        public MyICommand Next { get; set; }
        public MyICommand ContinueRegisterCommand { get; set; }
        public MyICommand<string> SelectGenderCommand { get; set; }

        private string patientGender;
        private DateTime patientBirthday;
        private string msText;
        private string esText;

        public RegisterPatientView2Model()
        {
            _app = Application.Current as App;
            HelpText = "You can see the list of scheduled examinations for the chosen parameters, click on button 'See the List' to see it.";
            ContinueRegisterCommand = new MyICommand(OnContinueRegister, CanContinue);
            SelectGenderCommand = new MyICommand<string>(OnSelectGenderCommand);
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;

            PatientBirthday = DateTime.Now;
            // Blood Type
            BloodTypeOptions = new ObservableCollection<BloodType>();
            AddBloodTypesOptions();
            // Allergies
            AllergiesOptions = new ObservableCollection<string>();
            AddAllergiesOptions();

        }

        #region Data
        public void AddBloodTypesOptions()
        {
            BloodTypeOptions.Add(new BloodType("A+"));
            BloodTypeOptions.Add(new BloodType("A-"));
            BloodTypeOptions.Add(new BloodType("B+"));
            BloodTypeOptions.Add(new BloodType("B-"));
            BloodTypeOptions.Add(new BloodType("AB+"));
            BloodTypeOptions.Add(new BloodType("AB-"));
            BloodTypeOptions.Add(new BloodType("0+"));
            BloodTypeOptions.Add(new BloodType("0-"));

            SelectedBloodType = new BloodType("A+");
            OnPropertyChanged("BloodTypeOptions");
        }
        public void AddAllergiesOptions()
        {
            AllergiesOptions.Add("Penicillin");
            AllergiesOptions.Add("Ibuprofen");
            AllergiesOptions.Add("Aspirin");
            SelectedAllergy = "Penicillin";

            OnPropertyChanged("AllergiesOptions");
        }
        #endregion

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
        public BloodType SelectedBloodType
        {
            get { return selectedBloodType; }
            set
            {
                if (selectedBloodType != value)
                {
                    selectedBloodType = value;
                    OnPropertyChanged("SelectedBloodType");
                }
            }
        }
        public string SelectedAllergy
        {
            get { return selectedAllergy; }
            set
            {
                if (selectedAllergy != value)
                {
                    selectedAllergy = value;
                    OnPropertyChanged("SelectedAllergy");
                }
            }
        }
        public string PatientGender
        {
            get { return patientGender; }
            set
            {
                if (patientGender != value)
                {
                    patientGender = value;
                    OnPropertyChanged("PatientGender");
                    ContinueRegisterCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime PatientBirthday
        {
            get { return patientBirthday; }
            set
            {
                if (patientBirthday != value)
                {
                    patientBirthday = value;
                    OnPropertyChanged("PatientBirthday");
                }
            }
        }
        public string MSText
        {
            get { return msText; }
            set
            {
                if (msText != value)
                {
                    msText = value;
                    OnPropertyChanged("MSText");
                }
            }
        }
        public string ESText
        {
            get { return esText; }
            set
            {
                if (esText != value)
                {
                    esText = value;
                    OnPropertyChanged("ESText");
                }
            }
        }

        #endregion

        #region Handlers

        private void OnSelectGenderCommand(string gender)
        {
            PatientGender = gender;
        }
        private void OnContinueRegister()
        {
            _app.TempPatient.Gender = PatientGender;
            _app.TempPatient.BirthDate = PatientBirthday;

            _app.TempPatient.BloodType = SelectedBloodType;

            _app.PatientController.Create(_app.TempPatient);

            mainWindowDataContext.OnNav("register_patient3");
        }

        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            ErrorText = "";
            if(PatientGender == null)
            {
                ErrorText = "Select gender to continue.";
                return false;
            }
            return true;
        }
      
        #endregion

    }
}
