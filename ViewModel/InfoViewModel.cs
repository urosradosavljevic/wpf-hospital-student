using System;

namespace Bolnica.ViewModel
{
    class InfoViewModel : BindableBase
    {

        MainWindowViewModel mainWindowDataContext;

        public string HelpText { get; set; }
        private string infoTitle;
        private string infoContent;
        public InfoViewModel()
        {
            HelpText = "You can see the list of scheduled examinations for the chosen parameters, click on button 'See the List' to see it.";
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;
            InfoTitle = "Your feedback will be reviewed soon";
            InfoContent = "Thank you for your feedback.";
        }
        public string InfoTitle
        {
            get { return infoTitle; }
            set
            {
                infoTitle = value;
                OnPropertyChanged("InfoTitle");
            }
        }
        public string InfoContent
        {
            get { return infoContent; }
            set
            {
                infoContent = value;
                OnPropertyChanged("InfoContent");
            }
        }
        public void SetInfoText(string title, string content)
        {
            InfoTitle = title;
            InfoContent = content;
        }
    }
}
