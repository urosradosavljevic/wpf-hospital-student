using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica.ViewModel
{
    class ScheduleApointementSubmitedViewModel : BindableBase
    {
        private readonly App _app;
        public string HelpText { get; set; }
        public string EmergencyText { get; set; }
        private MainWindowViewModel mainWindowDataContext;

        public MyICommand GoHomeCommand { get; set; }

        public string SubmitStatusText { get; set; }
        public string TypeOfIntervention { get; set; }
        public string DoctorFullName { get; set; }
        public string ApointementDate { get; set; }
        public string ApointementTime { get; set; }
        public string ApointementRoom { get; set; }


        public ScheduleApointementSubmitedViewModel()
        {
            HelpText = "Schedule Apointement Submit Help text";
            mainWindowDataContext = App.Current.MainWindow.DataContext as MainWindowViewModel;

            GoHomeCommand = new MyICommand(OnGoHome);

            SubmitStatusText = "You have successfully scheduled an examination!";

            _app = Application.Current as App;
            TypeOfIntervention = _app.TempAppointement.Intervention;
            DoctorFullName = _app.TempAppointement.Doctor.FullName;
            ApointementDate = _app.TempAppointement.Date.ToString("dd.MM.yyyy.");
            ApointementTime = _app.TempAppointement.Term.Value;
            ApointementRoom = _app.TempAppointement.Emergency ? "You will be assigned with room ASAP." : _app.TempAppointement.Room.RoomNumber.ToString();

            _app.AppointementController.Create(_app.TempAppointement);
        }
        #region Handlers

        private void OnGoHome()
        {
            mainWindowDataContext.OnNav("home");
        }
        #endregion
    }
}
