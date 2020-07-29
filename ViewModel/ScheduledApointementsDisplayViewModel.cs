using Bolnica.Model;
using Bolnica.Model.Util;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ScheduledApointementsDisplayViewModel : BindableBase
    {
        public string HelpText { get; set; }
        private readonly App _app;
        MainWindowViewModel mainWindowDataContext;

        private string errorText;
        private bool flagSubmit;

        private Appointement selectedApointement;
        private Doctor selectedDoctor;
        private DateTime selectedDate;
        private Term selectedTerm;
        // Apointements
        private ObservableCollection<Appointement> apointements;
        public ObservableCollection<Doctor> DoctorsOptions { get; set; }
        public ObservableCollection<DateTime> DatesOptions { get; set; }
        public ObservableCollection<Term> TermOptions { get; set; }
        public ObservableCollection<Appointement> FilteredApointements { get; set; }

        // Commands
        public MyICommand ChangeDoctorCommand { get; set; }
        public MyICommand ChangeTermCommand { get; set; }
        public MyICommand CheckCancelApointementCommand { get; set; }
        public MyICommand CancelApointementCommand { get; set; }
        public MyICommand<object> SelectTermCommand { get; set; }


        public ScheduledApointementsDisplayViewModel()
        {
            _app = Application.Current as App;
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            HelpText = "You can see the list of scheduled examinations for the chosen parameters, click on button 'See the List' to see it.";

            var TempScheduled = _app.TempScheduled;

            if (TempScheduled is null)
            {
                AddApointements();
            }
            else
            {
                Apointements = new ObservableCollection<Appointement>(TempScheduled);
            }

            DoctorsOptions = new ObservableCollection<Doctor>();
            DatesOptions = new ObservableCollection<DateTime>();
            TermOptions = new ObservableCollection<Term>();
            FilteredApointements = new ObservableCollection<Appointement>();

            FlagSubmit = false;

            //SelectedDate = mainWindowDataContext.CashedApointement.Term.Date;

            SelectTermCommand = new MyICommand<object>(OnSelectTerm);

            ChangeDoctorCommand = new MyICommand(OnChangeDoctor, CanChange);
            ChangeTermCommand = new MyICommand(OnChangeTerm, CanChange);

            CheckCancelApointementCommand = new MyICommand(OnCheck, CanChange);
            CancelApointementCommand = new MyICommand(OnCancelApointement, IsChecked);

            // AddDoctorsOptions();

        }

        private bool IsChecked() { return FlagSubmit; }
        private void OnCheck() { FlagSubmit = true; }

        #region Getters & Setters
        public bool FlagSubmit
        {
            get { return flagSubmit; }
            set
            {
                if (flagSubmit != value)
                {
                    flagSubmit = value;
                    OnPropertyChanged("FlagSubmit");
                    CancelApointementCommand.RaiseCanExecuteChanged();
                }
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
                }
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
                OnPropertyChanged("ErrorText");
            }
        }
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged("SelectedDate");
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
                    SelectedAppointementChanged();
                }
            }
        }
        public ObservableCollection<Appointement> Apointements
        {
            get { return apointements; }
            set
            {
                if (apointements != value)
                {
                    apointements = value;
                    OnPropertyChanged("Apointements");
                }
            }
        }
        public Term SelectedTerm
        {
            get { return selectedTerm; }
            set
            {
                if (selectedTerm != value)
                {
                    selectedTerm = value;
                    OnPropertyChanged("SelectedTerm");
                }
            }
        }
        #endregion

        #region Handlers
        public void OnSelectTerm(object parameter)
        {

            var values = (object[])parameter;

            var id = (int)values[0];
            var column= (int)values[1];

            DateTime SelectedDate = new DateTime(2020, 6, 21);

            DateTime dateTime;
            if(column == 1)
            {
                dateTime = SelectedDate;
            }else if(column == 0)
            {
                dateTime = SelectedDate.AddDays(-1);
            }
            else {
                dateTime = SelectedDate.AddDays(1);
            }


            mainWindowDataContext.BackNavRoute = "scheduled_apointements_display";
            mainWindowDataContext.BackButtonEnabled = true;

            // SelectedApointement = Apointements.Single(ap => ap.Term.Value == id && ap.Date == dateTime.Date);
        }
        public void OnChangeTerm()
        {
            _app.TempAppointement = SelectedApointement;

            mainWindowDataContext.OnNav("scheduled_apointements_term_change");
        }
        public void SelectedAppointementChanged()
        {
            OnPropertyChanged("SelectedApointement");
            ChangeDoctorCommand.RaiseCanExecuteChanged();
            ChangeTermCommand.RaiseCanExecuteChanged();
            CancelApointementCommand.RaiseCanExecuteChanged();
            CheckCancelApointementCommand.RaiseCanExecuteChanged();
            FlagSubmit = false;
        }
        public void OnChangeDoctor()
        {
            _app.TempAppointement = SelectedApointement;
            mainWindowDataContext.OnNav("scheduled_apointements_doctor_change");
        }
        public void OnCancelApointement()
        {
            _app.AppointementController.Delete(SelectedApointement); Console.WriteLine("Cancel");

            SelectedApointement = null;
        }
        #endregion

        #region Data
        
        public void AddApointements()
        {
            Console.WriteLine("AddApointements called");

            Apointements = new ObservableCollection<Appointement>(_app.AppointementController.GetAll());
            OnPropertyChanged("Apointements");
        }
        #endregion


        #region Validator Implementation
        private bool CanChange()
        {
            ErrorText = "";
            if (SelectedApointement is null)
            {
                ErrorText = "Select apointement to continue.";
                return false;
            }
            return true;
        }
        #endregion
    }
}