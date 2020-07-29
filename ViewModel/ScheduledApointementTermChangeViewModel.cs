using Bolnica.Model;
using Bolnica.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ScheduledApointementTermChangeViewModel : BindableBase
    {
        private readonly App _app;
        public string HelpText { get; set; }
        MainWindowViewModel mainWindowDataContext;

        private string errorText;

        // Commands
        public MyICommand GoToNextPageCommand { get; set; }

        // Term
        public ObservableCollection<Term> termOptionsList;
        public Term selectedTerm;

        public ObservableCollection<DateTime> dateSortingOptions;
        private DateTime selectedDateFilter;


        // Apointemet
        public Appointement ApointementToChange { get; set; }

        public ScheduledApointementTermChangeViewModel()
        {
            HelpText = "Click on Term to select";
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            _app = Application.Current as App;

            SelectedDateFilter = _app.TempAppointement.Date;

            GoToNextPageCommand = new MyICommand(OnGoToNextPage, CanContinue);

            AddDateSortingOptions();
            
        }

        #region Getters & Setters

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

        #endregion

        #region Data
        public void AddDateSortingOptions()
        {
            DateSortingOptions = new ObservableCollection<DateTime>();

            var dayBefore = DateTime.Today.AddDays(-1);
            var chosenDay = DateTime.Today;
            var dayAfter = DateTime.Today.AddDays(1);

            DateSortingOptions.Add(dayBefore);
            DateSortingOptions.Add(chosenDay);
            DateSortingOptions.Add(dayAfter);
        }

        public void AddTermOptions()
        {
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

        #endregion

        #region Handlers

        public void FilterTermsByDate()
        {
            AddTermOptions();

            var appointements = _app.AppointementController.GetAppointementsByDate(SelectedDateFilter).ToList();
            // Filter Terms
            TermOptionsList = new ObservableCollection<Term>(from t in TermOptionsList where AppointementWithSpecificTermExists(appointements, t) select t);
            Console.WriteLine("Filtered");
        }

        private bool AppointementWithSpecificTermExists(IEnumerable<Appointement> appointements, Term term)
        {
            if (appointements.Count() == 0) return true;
            var app = appointements.FirstOrDefault(ap => ap.Term.Value == term.Value);
            if (app is null) return true;
            return false;
        }

        public void OnGoToNextPage()
        {
            _app.TempAppointement.Term = SelectedTerm;
            _app.AppointementController.Update(_app.TempAppointement);
            
            mainWindowDataContext.OnNav("scheduled_apointements_display");            
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
