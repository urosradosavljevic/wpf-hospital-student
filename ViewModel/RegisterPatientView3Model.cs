using System.Windows;

namespace Bolnica.ViewModel
{
    class RegisterPatientView3Model : BindableBase
    {
        private readonly App _app;
        public string HelpText { get; set; }

        public string SubmitStatusText { get; set; }
        public string SubmitStatusDescription { get; set; }

        public RegisterPatientView3Model()
        {
            _app = Application.Current as App;
            HelpText = "Register Patient Submited Help text";

            var patient = _app.TempPatient.FullName;

            SubmitStatusText = "You have successfully registered " + patient;
            SubmitStatusDescription = "You can schedule an examination for new registered (this) patient Schedule an Examination or only Back to the menu on buttons below.";
            _app.PatientController.Create(_app.TempPatient);
        }

    }
}
