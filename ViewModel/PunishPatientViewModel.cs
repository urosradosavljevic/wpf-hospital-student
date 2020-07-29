
using Bolnica.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class PunishPatientViewModel : BindableBase
    {
        private App app;
        public string HelpText { get; set; }
        MainWindowViewModel mainWindowDataContext;

        private string errorText;

        private Patient selectedPatient;

        // Apointements
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Patient> FilteredPatients { get; set; }

        // Commands
        public MyICommand SubmitPunishCommand { get; set; }

        public PunishPatientViewModel()
        {
            app = Application.Current as App;
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            HelpText = "You can see the list of scheduled examinations for the chosen parameters, click on button 'See the List' to see it.";

            SubmitPunishCommand = new MyICommand(OnSubmit, CanSubmit);


            AddPatients();

            FilteredPatients = Patients;

            FilterPatients();           
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
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                if (selectedPatient != value)
                {
                    selectedPatient = value;
                    OnPropertyChanged("SelectedPatient");
                    SubmitPunishCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Handlers
        public void OnSubmit()
        {
            int idx = FilteredPatients.IndexOf(SelectedPatient);

            if (idx >= 0)
            {
                FilteredPatients[idx].Punished = true;
                var patient = FilteredPatients[idx];
                app.PatientController.Update(patient);
            }

            FilterPatients();
            SelectedPatient = null;
            OnPropertyChanged("FilteredPatients");
        }
        #endregion

        #region Data
        public void FilterPatients()
        {
            FilteredPatients = new ObservableCollection<Patient>(from patient in FilteredPatients where !patient.Punished && patient.Violated select patient);
            OnPropertyChanged("FilteredPatients");
        }
        
        public void AddPatients()
        {
            Patients = new ObservableCollection<Patient>(app.PatientController.GetPatientsToPunish());
            OnPropertyChanged("Patients");
        }
        
        #endregion

        #region Validator Implementation
        private bool CanSubmit()
        {
            ErrorText = "";
            if (SelectedPatient is null)
            {
                ErrorText = "Select patient to continue.";
                return false;
            }
            return true;
        }
        #endregion
    }
}