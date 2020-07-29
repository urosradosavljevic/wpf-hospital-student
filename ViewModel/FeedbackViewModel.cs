using Bolnica.Controller;
using Bolnica.Model;
using System;
using System.Windows;

namespace Bolnica.ViewModel
{
    class FeedbackViewModel : BindableBase
    {
        public MyICommand SubmitFeedbackCommand { get; set; }
        MainWindowViewModel mainWindowDataContext;

        private readonly App _app;

        public string HelpText { get; set; }
        private string feedbackText;
        public FeedbackViewModel()
        {
            HelpText = "You can see the list of scheduled examinations for the chosen parameters, click on button 'See the List' to see it.";
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;

            _app = Application.Current as App;

            SubmitFeedbackCommand = new MyICommand(OnSubmitFeedback);
        }

        public string FeedbackText 
            {
            get {
                return feedbackText;
            }
            set
            {
                feedbackText = value;
                OnPropertyChanged("FeedbackText");
            }
            }

        private void OnSubmitFeedback() 
        {
            _app.FeedbackController.Create(new Feedback(DateTime.Now, FeedbackText));

            mainWindowDataContext.OnNav("info");
        }
    }
}
