using Bolnica.Model;
using System;
using System.Windows;

namespace Bolnica.ViewModel
{
    class HomeViewModel:BindableBase
    {
        private string welcomeText;

        public string HelpText { get; set; }
        public HomeViewModel()
        {
            HelpText = "You can choose some of functions from menu(up left) to do something. ";
        }
        public string WelcomeText
        {
            get { return "Hi, Secretary. We welcome you!"; }
            set
            {
                if (welcomeText != value)
                {
                    welcomeText = value;
                    OnPropertyChanged("WelcomeText");
                }
            }
        }
    }
}
