

using Bolnica.Controller;
using Bolnica.Controller.Abstract;
using Bolnica.Model;
using Bolnica.Model.Util;
using Bolnica.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Bolnica
{
    class MainWindowViewModel : BindableBase
    {
        // Command 
        public MyICommand MenuOpenCommand { get; private set; }
        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand<string> ShowHelpCommand { get; private set; }
        public MyICommand CloseHelpCommand { get; private set; }

        // Global variables
        private BindableBase currentViewModel;
        private string headerLabelText;
        private string helpText;
        private string backNavRoute;

        public bool backButtonEnabled;
        public bool loginFlag;
        public bool menuOpenFlag;

        private readonly IAppointementController _appointementController;
        private readonly IController<Doctor, long> _doctorController;
        private readonly IPatientController _patientController;
        private readonly IController<Room, long> _roomController;
        private readonly IController<Feedback, long> _feedbackController;
        private readonly IController<Secretary, long> _secretaryController;

        // Screens *******
        // Register
        private RegisterPatientView1Model registerPatient1ViewModel;
        private RegisterPatientView2Model registerPatient2ViewModel;
        private RegisterPatientView3Model registerPatient3ViewModel;
        // Login
        private LoginViewModel loginViewModel;
        // Home
        private HomeViewModel homeViewModel;
        // Punish Patient
        private PunishPatientViewModel punishPatientViewModel;
        // Feedback
        private FeedbackViewModel feedbackViewModel;
        // Feedback Info
        private InfoViewModel infoViewModel;
        // Account Info
        private AccountInfoViewModel accountInfoViewModel;
        // Assign Room
        private AssignRoomViewModel assignRoomViewModel;
        // Schedule Apointement With Account
        private ApointementWithoutAccStartViewModel apointementWithoutAccStartViewModel;       
        // Schedule Apointement With Account
        private ApointementWithAccStartViewModel apointementWithAccStartViewModel;
        // Schedule Apointement Shared
        private ScheduleApointementPeriodDoctorViewModel scheduleApointementPeriodDoctorViewModel;
        private ScheduleApointementTermViewModel scheduleApointementTermViewModel;
        private ScheduleApointementRoomViewModel scheduleApointementRoomViewModel;
        private ScheduleApointementSubmitedViewModel scheduleApointementSubmitedViewModel;
        // Scheduled Examinations
        private ScheduledApointementsStartViewModel scheduledApointementsStartViewModel;
        private ScheduledApointementsDisplayViewModel scheduledApointementsDisplayViewModel;
        private ScheduledApointementTermChangeViewModel scheduledApointementTermChangeViewModel;
        private ScheduledApointementDoctorChangeViewModel scheduledApointementDoctorChangeViewModel;
        
        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            MenuOpenCommand = new MyICommand(OnOpen);

            loginViewModel = new LoginViewModel();
            CurrentViewModel = loginViewModel;
            HelpText = loginViewModel.HelpText;
            HeaderLabelText = "Login";

            var app = Application.Current as App;
            _appointementController = app.AppointementController;
            _feedbackController = app.FeedbackController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
            _roomController = app.RoomController;
        }

        private void OnOpen()
        {
            MenuOpenFlag = true;
        }

        public void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    homeViewModel = new HomeViewModel();
                    BackButtonEnabled = false;
                    HelpText = homeViewModel.HelpText;
                    HeaderLabelText = "Home";

                    CurrentViewModel = homeViewModel;
                    break;
                    // Register
                case "register_patient":
                    registerPatient1ViewModel = new RegisterPatientView1Model();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Register - User Info";
                    HelpText = registerPatient1ViewModel.HelpText;
                    
                    CurrentViewModel = registerPatient1ViewModel;
                    break;
                case "register_patient2":
                    registerPatient2ViewModel = new RegisterPatientView2Model();
                    BackNavRoute = "register_patient";
                    BackButtonEnabled = true;
                    HeaderLabelText = "Register - Med Info";
                    HelpText = registerPatient2ViewModel.HelpText;
                    
                    CurrentViewModel = registerPatient2ViewModel;
                    break;
                case "register_patient3":
                    registerPatient3ViewModel = new RegisterPatientView3Model();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Register - Submit";
                    HelpText = registerPatient3ViewModel.HelpText;
                    
                    CurrentViewModel = registerPatient3ViewModel;
                    break;
                    // Assign Room
                case "assign_room":
                    assignRoomViewModel = new AssignRoomViewModel();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Assign Apointement Room";
                    HelpText = assignRoomViewModel.HelpText;
                    
                    CurrentViewModel = assignRoomViewModel;
                    break;
                    // Punish Patient
                case "punish_patient":
                    punishPatientViewModel = new PunishPatientViewModel();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Punish patient";
                    HelpText = punishPatientViewModel.HelpText;
                    
                    CurrentViewModel = punishPatientViewModel;
                    break;
                    // Scheduled Apointements
                case "scheduled_apointements":
                    scheduledApointementsStartViewModel = new ScheduledApointementsStartViewModel();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Scheduled Apointements";
                    HelpText = scheduledApointementsStartViewModel.HelpText;
                    
                    CurrentViewModel = scheduledApointementsStartViewModel;
                    break;
                case "scheduled_apointements_display":
                    scheduledApointementsDisplayViewModel = new ScheduledApointementsDisplayViewModel();
                    BackNavRoute = "scheduled_apointements";
                    BackButtonEnabled = true;
                    HeaderLabelText = "Scheduled Apointements - Display";
                    HelpText = scheduledApointementsDisplayViewModel.HelpText;
                    
                    CurrentViewModel = scheduledApointementsDisplayViewModel;
                    break;
                case "scheduled_apointements_term_change":
                    scheduledApointementTermChangeViewModel = new ScheduledApointementTermChangeViewModel();
                    BackNavRoute = "scheduled_apointements_display";
                    BackButtonEnabled = true;
                    HeaderLabelText = "Scheduled Apointements - Term Change";
                    HelpText = scheduledApointementTermChangeViewModel.HelpText;

                    CurrentViewModel = scheduledApointementTermChangeViewModel;
                    break;
                case "scheduled_apointements_doctor_change":
                    scheduledApointementDoctorChangeViewModel = new ScheduledApointementDoctorChangeViewModel();
                    BackNavRoute = "scheduled_apointements_display";
                    BackButtonEnabled = true;
                    HeaderLabelText = "Scheduled Apointements - Doctor Change";
                    HelpText = scheduledApointementDoctorChangeViewModel.HelpText;

                    CurrentViewModel = scheduledApointementDoctorChangeViewModel;
                    break;
                // Schedule Apointements 
                case "apointement_with_account":
                    apointementWithAccStartViewModel = new ApointementWithAccStartViewModel();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Apointement - Existing Patient";
                    HelpText = apointementWithAccStartViewModel.HelpText;
                    
                    CurrentViewModel = apointementWithAccStartViewModel;
                    break;
                case "apointement_without_account":
                    apointementWithoutAccStartViewModel = new ApointementWithoutAccStartViewModel();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Apointement - New Patient";
                    HelpText = apointementWithoutAccStartViewModel.HelpText;
                    
                    CurrentViewModel = apointementWithoutAccStartViewModel;
                    break;
                // Schedule Apointement Shared
                case "schedule_apointement_period_doctor":
                    scheduleApointementPeriodDoctorViewModel = new ScheduleApointementPeriodDoctorViewModel();
                    BackNavRoute = "apointement_with_account";
                    BackButtonEnabled = true;
                    HeaderLabelText = "Apointement - Doctor";
                    HelpText = scheduleApointementPeriodDoctorViewModel.HelpText;

                    CurrentViewModel = scheduleApointementPeriodDoctorViewModel;
                    break;
                case "schedule_apointement_term":
                    scheduleApointementTermViewModel = new ScheduleApointementTermViewModel();
                    BackNavRoute = "schedule_apointement_period_doctor";
                    BackButtonEnabled = true;
                    HeaderLabelText = "Apointement - Term";
                    HelpText = scheduleApointementTermViewModel.HelpText;

                    CurrentViewModel = scheduleApointementTermViewModel;
                    break;
                case "schedule_apointement_room":
                    scheduleApointementRoomViewModel= new ScheduleApointementRoomViewModel();
                    BackNavRoute = "schedule_apointement_term";
                    BackButtonEnabled = true;
                    HeaderLabelText = "Apointement - Room";
                    HelpText = scheduleApointementRoomViewModel.HelpText;

                    CurrentViewModel = scheduleApointementRoomViewModel;
                    break;
                case "schedule_apointement_submit":
                    scheduleApointementSubmitedViewModel = new ScheduleApointementSubmitedViewModel();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Apointement - Submit";
                    HelpText = scheduleApointementSubmitedViewModel.HelpText;
                    
                    CurrentViewModel = scheduleApointementSubmitedViewModel;
                    break;
                // Info 
                case "info":
                    infoViewModel = new InfoViewModel();
                    BackButtonEnabled = false;
                    HeaderLabelText = "Feedback Info";
                    
                    CurrentViewModel = infoViewModel;
                    break;
                // Account Info
                case "account_info":
                    accountInfoViewModel = new AccountInfoViewModel();
                    BackButtonEnabled = false;
                    HelpText = accountInfoViewModel.HelpText;
                    HeaderLabelText = "Account Info";
                    
                    CurrentViewModel = accountInfoViewModel;
                    break;
                // Feedback 
                case "feedback":
                    feedbackViewModel = new FeedbackViewModel();
                    BackButtonEnabled = false;
                    HelpText = feedbackViewModel.HelpText;
                    HeaderLabelText = "Feedback";
                    
                    CurrentViewModel = feedbackViewModel;
                    break;
                case "sign_out":
                    loginViewModel = new LoginViewModel();
                    HelpText = loginViewModel.HelpText;
                    BackButtonEnabled = false;
                    LoginFlag = false;
                    HeaderLabelText = "Login";
                    
                    CurrentViewModel = loginViewModel;
                    break;
                case "exit":
                    App.Current.Shutdown();
                    break;
                default:

                    break;
            }
        }

        #region Getters & Setters

        public string HeaderLabelText
        {
            get { return headerLabelText; }
            set
            {
                if (headerLabelText != value)
                {
                    headerLabelText = value;
                    OnPropertyChanged("HeaderLabelText");
                }
            }
        }        
        public bool MenuOpenFlag
        {
            get { return menuOpenFlag; }
            set
            {
                if (menuOpenFlag != value)
                {
                    menuOpenFlag = value;
                    OnPropertyChanged("MenuOpenFlag");
                }
            }
        }   
        public string HelpText
        {
            get { return helpText; }
            set
            {
                if (helpText != value)
                {
                    helpText = value;
                    OnPropertyChanged("HelpText");
                }
            }
        }        
        public bool BackButtonEnabled
        {
            get { return backButtonEnabled; }
            set
            {
                if (backButtonEnabled != value)
                {
                    backButtonEnabled = value;
                    OnPropertyChanged("BackButtonEnabled");
                }
            }
        }
        public bool LoginFlag
        {
            get { return loginFlag; }
            set
            {
                if (loginFlag != value)
                {
                    loginFlag = value;
                    OnPropertyChanged("LoginFlag");
                }
            }
        }
        public string BackNavRoute
        {
            get { return backNavRoute; }
            set
            {
                if (backNavRoute != value)
                {
                    backNavRoute = value;
                    OnPropertyChanged("BackNavRoute");
                }
            }
        }
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        #endregion

    }
}
