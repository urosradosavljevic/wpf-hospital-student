using Bolnica.Model;
using System;
using System.Linq;
using System.Windows;

namespace Bolnica.ViewModel
{
    class AccountInfoViewModel : BindableBase
    {
        MainWindowViewModel mainWindowDataContext;
        
        public string HelpText { get; set; }
        private readonly App app;

        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string address;
        private DateTime birthDate;

        public MyICommand GoHomeCommand { get; set; }

        private Secretary currentSecretary;
        public AccountInfoViewModel()
        {
            HelpText = "Schedule Apointement Submit Help text";

            mainWindowDataContext = Application.Current.MainWindow.DataContext as MainWindowViewModel;
            app = Application.Current as App;

            Email = "MA daj";
            GoHomeCommand = new MyICommand(OnGoHome);

            SetUser();
        }

        #region Getters & Setters
        public Secretary CurrentSecretary
        {
            get
            {
                return currentSecretary;
            }
            set
            {
                currentSecretary = value;
                OnPropertyChanged("CurrentSecretary");
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        #endregion

        #region Data

        public void SetUser()
        {
            Console.WriteLine(app.TempSecretary.Email.Value);
            CurrentSecretary = app.TempSecretary;
        }
        #endregion

        #region Handlers

        private void OnGoHome()
        {
            mainWindowDataContext.OnNav("home");
        }
        #endregion
    }
}
