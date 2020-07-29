using Bolnica.Model;
using Bolnica.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bolnica.ViewModel
{
    class ScheduleApointementTermViewModel:BindableBase
    {
        public string HelpText { get; set; }

        MainWindowViewModel mainWindowDataContext;
        private readonly App _app;
        private const string DATETIME_FORMAT = "dd.MM.yyyy.";

        private bool emergency;
        private string errorText;
        private string buttonText;

        // Commands
        public MyICommand<bool> EmergencyFlagCommand { get; set; }
        public MyICommand GoToNextPageCommand { get; set; }

        // Term
        public ObservableCollection<Term> termOptionsList;
        public Term selectedTerm;

        public ObservableCollection<DateTime> dateSortingOptions;
        private DateTime selectedDateFilter;


        public ScheduleApointementTermViewModel()
        {
            HelpText = "Schedule Apointement Term Help text";
            ButtonText = "Submit";
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;


            GoToNextPageCommand = new MyICommand(OnGoToNextPage, CanContinue);
            EmergencyFlagCommand = new MyICommand<bool>(OnEmergencyChecked);

            _app = Application.Current as App;

            
            AddTermOptions();
            AddDateSortingOptions();
            SelectedDateFilter = _app.TempAppointement.Date;
            FilterTermsByDate();

        }

        #region Getters & Setters
            
        public string ButtonText
        {
            get
            {
                return buttonText;
            }
            set
            {
                buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }
        public bool Emergency
        {
            get
            {
                return emergency;
            }
            set
            {
                emergency = value;
                OnPropertyChanged("Emergency");
                ButtonText = value ? "Submit" : "Book a room";
                OnPropertyChanged("ButtonText");
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
        public Term SelectedTerm
        {
            get
            {
                return selectedTerm;
            }
            set
            {
                selectedTerm = value;
                OnPropertyChanged("SelectedTerm");
                GoToNextPageCommand.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<DateTime> DateSortingOptions
        {
            get
            {
                return dateSortingOptions;
            }
            set
            {
                dateSortingOptions = value;
                OnPropertyChanged("DateSortingOptions");
            }
        }
        public ObservableCollection<Term> TermOptionsList
        {
            get
            {
                return termOptionsList;
            }
            set
            {
                termOptionsList = value;
                OnPropertyChanged("TermOptionsList");
            }
        }
        public DateTime SelectedDateFilter
        {
            get
            {
                return selectedDateFilter;
            }
            set
            {
                selectedDateFilter = value;
                OnPropertyChanged("SelectedDateFilter");

                FilterTermsByDate();
            }
        }

        #endregion

        #region Data

        public void AddTermOptions()
        {
            // TODO: Add sorting options
            TermOptionsList = new ObservableCollection<Term>();
            TermOptionsList.Add(new Term("00:00-01:30"));
            TermOptionsList.Add(new Term("01:30-03:00"));
            TermOptionsList.Add(new Term("03:00-04:30"));
            TermOptionsList.Add(new Term("04:30-06:00"));
            TermOptionsList.Add(new Term("06:00-07:30"));
            TermOptionsList.Add(new Term("07:30-09:00"));
            TermOptionsList.Add(new Term("09:00-10:30"));
            TermOptionsList.Add(new Term("10:30-12:00"));
            TermOptionsList.Add(new Term("12:00-13:30"));
            TermOptionsList.Add(new Term("13:30-15:00"));
            TermOptionsList.Add(new Term("15:00-16:30"));
            TermOptionsList.Add(new Term("16:30-18:00"));
            TermOptionsList.Add(new Term("18:00-19:30"));
            TermOptionsList.Add(new Term("21:00-22:30"));
            TermOptionsList.Add(new Term("22:30-00:00"));
        }
        
        public void FilterTermsByDate()
        {
            AddTermOptions();

            var appointements = _app.AppointementController.GetAppointementsByDate(SelectedDateFilter).ToList();
            // Filter Terms
            TermOptionsList = new ObservableCollection<Term>(from t in TermOptionsList where AppointementWithSpecificTermExists(appointements,t) select t);
        }

        private bool AppointementWithSpecificTermExists(IEnumerable<Appointement> appointements,Term term)
        {
            if (appointements.Count() == 0) return true;
            var app = appointements.FirstOrDefault(ap => ap.Term.Value == term.Value);
            if (app is null) return true;
            return false;
        }

        public void AddDateSortingOptions()
        {
            DateSortingOptions = new ObservableCollection<DateTime>();
            
            var dayBefore = _app.TempAppointement.Date.AddDays(-1);
            var chosenDay = _app.TempAppointement.Date;
            var dayAfter = _app.TempAppointement.Date.AddDays(1);
            
            DateSortingOptions.Add(dayBefore);
            DateSortingOptions.Add(chosenDay);
            DateSortingOptions.Add(dayAfter);

            SelectedDateFilter = DateSortingOptions[1];
        }
        
        #endregion

        #region Handlers
        public void OnGoToNextPage()
        {
            if (Emergency)
            {
                _app.TempAppointement.Term = SelectedTerm;
                _app.TempAppointement.Date = SelectedDateFilter;
                _app.TempAppointement.Emergency = true;
                mainWindowDataContext.OnNav("schedule_apointement_submit");
            }
            else
            {
                _app.TempAppointement.Term = SelectedTerm;
                _app.TempAppointement.Date = SelectedDateFilter;
                mainWindowDataContext.OnNav("schedule_apointement_room");
            }
        }
        private void OnEmergencyChecked(bool parameter)
        {
            Emergency = parameter;
        }



        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            ErrorText = "";
            if (SelectedTerm is null)
            {
                ErrorText = "Select termin to continue.";
                return false;
            }
            return true;
        }
        #endregion
    }
}
