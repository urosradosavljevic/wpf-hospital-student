using Bolnica.Model;
using Bolnica.Model.Util;
using Bolnica.Validation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;

namespace Bolnica.ViewModel
{
    class RegisterPatientView1Model: BindableBase, IDataErrorInfo
    {
        public SecureString SecurePassword { private get; set; }

        RegisterPatient1Validator registerPatient1Validator;
        public MyICommand Next { get; set; }
        public MyICommandWithPassword ContinueCommand { get; set; }


        private readonly App _app;

        MainWindowViewModel mainWindowDataContext;
        private bool ValidationStatus { get; set; }

        private string fnText;
        private string lnText;
        private string emText;
        private string unText;
        private string password;
        private string confirmPassword;
        private string pnText;
        private string adrText;
        private string passwordErrorText;

        public string HelpText {get; set;}
        public RegisterPatientView1Model()
        {
            HelpText = "You can see the list of scheduled examinations for the chosen parameters, click on button 'See the List' to see it.";
            ContinueCommand = new MyICommandWithPassword(OnContinue, CanContinue);
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            
            registerPatient1Validator = new RegisterPatient1Validator();
            _app = Application.Current as App;
        }

        #region Getters & Setters

        public string FNText
        {
            get { return fnText; }
            set
            {
                if (fnText != value)
                {
                    fnText = value;
                    OnPropertyChanged("FNText");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string LNText
        {
            get { return lnText; }
            set
            {
                if (lnText != value)
                {
                    lnText = value;
                    OnPropertyChanged("LNText");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string EMText
        {
            get { return emText; }
            set
            {
                if (emText != value)
                {
                    emText = value;
                    OnPropertyChanged("EMText");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string UNText
        {
            get { return unText; }
            set
            {
                if (unText != value)
                {
                    unText = value;
                    OnPropertyChanged("UNText");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string Password
        {
            private get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string ConfirmPassword
        {
            private get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string PNText
        {
            get { return pnText; }
            set
            {
                if (pnText != value)
                {
                    pnText = value;
                    OnPropertyChanged("PNText");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string ADRText
        {
            get { return adrText; }
            set
            {
                if (adrText != value)
                {
                    adrText = value;
                    OnPropertyChanged("ADRText");
                    ContinueCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string PasswordErrorText
        {
            get
            {
                return passwordErrorText;
            }
            set
            {
                passwordErrorText = value;
                OnPropertyChanged("PasswordErrorText");
            }
        }
        #endregion
            
        #region Handlers

        private void OnContinue(object parameter)
        {
            if (Password != ConfirmPassword)
            {
                return;
            }

            _app.TempPatient = new Patient()
            {
                OneTimePatient = false,
                Violated = false,
                Punished = false,
                FirstName = FNText,
                LastName = LNText,
                Email = new Email(EMText),
                PhoneNumber = new PhoneNumber(int.Parse(PNText)),
                Address = ADRText,
                Username = UNText,
                Password = new Password(Password)
            };

            mainWindowDataContext.OnNav("register_patient2");
        }

        #endregion

        #region Validator Implementation
        private bool CanContinue()
        {
            PasswordErrorText = "";
            if (Password != ConfirmPassword)
            {
                PasswordErrorText = "Passwords must match.";
            }
            if(Password != null)
            {
                if (Password.Length < 5)
                {
                    PasswordErrorText = "Passwords must be longer than 5 characters.";
                }
                else if (Password.Length > 12) 
                {
                    PasswordErrorText = "Passwords must be shorter than 12 characters.";
                }
                var r = new Regex(@".*[0-9].*");
                if (!r.IsMatch(Password))
                {
                    PasswordErrorText = "Passwords must containt at least one number.";
                }
            }

            return registerPatient1Validator.Validate(this).IsValid;
        }
        public string Error
        {
            get
            {
                if (registerPatient1Validator != null)
                {
                    var results = registerPatient1Validator.Validate(this);
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
                var firstOrDefault = registerPatient1Validator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return registerPatient1Validator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
        #endregion

    }
}
