using Bolnica.Controller;
using Bolnica.Controller.Abstract;
using Bolnica.Model;
using Bolnica.Model.Util;
using Bolnica.Validation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bolnica.ViewModel
{
    public class LoginViewModel : BindableBase, IDataErrorInfo
    {
        LoginUserValidator _loginUserValidor;
        MainWindowViewModel mainWindowDataContext;

        private string emText;
        private string loginErrorText;
        private string passText;

        public MyICommandWithPassword LoginCommand { get; set; }

        private readonly App app;

        public string HelpText { get; set; }
        
        public LoginViewModel()
        {
            LoginErrorText = "";
            HelpText = "Hospital Secretary Login Page.";
            LoginCommand = new MyICommandWithPassword(OnLogin, CanLogin);
            _loginUserValidor = new LoginUserValidator();
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;

            app = Application.Current as App;
        }

        #region Getters & Setters
        public string EMText
        {
            get { return emText; }
            set
            {
                if (emText != value)
                {
                    emText = value;
                    OnPropertyChanged("EMText");
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string PASSText
        {
            get { return passText; }
            set
            {
                if (passText != value)
                {
                    passText = value;
                }
            }
        }        
        public string LoginErrorText
        {
            get { return loginErrorText; }
            set
            {
                if (loginErrorText != value)
                {
                    loginErrorText = value;
                    OnPropertyChanged("LoginErrorText");
                }
            }
        }
        #endregion

        private void OnLogin(PasswordBox parameter)
        {

            var o = app.SecretaryController.GetSecretaryByEmail(EMText);
            
            // TODO: Implement login
            if(o is null)
            {
                LoginErrorText = "Secretary with that email doesn't exist.";
                return;
            }
            if (o.Password.Value == parameter.Password)
            {
                app.TempSecretary = o;
                mainWindowDataContext.LoginFlag = true;
                mainWindowDataContext.OnNav("home");
            }
            else
            {
                LoginErrorText  = "Invalid password";
                return;
            }
            

        }

        private bool CanLogin()
        {
            return _loginUserValidor.Validate(this).IsValid;
        }

        
        #region // LoginUserValidator implementation
        public string Error
        {
            get
            {
                if (_loginUserValidor != null)
                {
                    var results = _loginUserValidor.Validate(this);
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
                var firstOrDefault = _loginUserValidor.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _loginUserValidor != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }

        #endregion

    }
}
