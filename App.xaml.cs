using Bolnica.Controller;
using Bolnica.Controller.Abstract;
using Bolnica.Model;
using Bolnica.Model.Util;
using Bolnica.Repository;
using Bolnica.Repository.CSV.Converter;
using Bolnica.Repository.CSV.Stream;
using Bolnica.Repository.Sequencer;
using Bolnica.Service;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private const string DOCTOR_FILE = "../../Resources/Data/doctors.csv";
        private const string PATIENT_FILE = "../../Resources/Data/patients.csv";
        private const string ROOM_FILE = "../../Resources/Data/rooms.csv";
        private const string APPOINTEMENT_FILE = "../../Resources/Data/appointements.csv";
        private const string SECRETARY_FILE = "../../Resources/Data/secretaries.csv";
        private const string FEEDBACK_FILE = "../../Resources/Data/feedback.csv";
        private const string CSV_DELIMITER = ",";

        private const string DATETIME_FORMAT = "dd.MM.yyyy.";
            
        public App()
        {
            var doctorRepository = new DoctorRepository(
                new CSVStream<Doctor>(DOCTOR_FILE, new DoctorCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
                new LongSequencer());
            var patientRepository = new PatientRepository(
                new CSVStream<Patient>(PATIENT_FILE, new PatientCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
                new LongSequencer());
            var roomRepository = new RoomRepository(
                new CSVStream<Room>(ROOM_FILE, new RoomCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
                new LongSequencer());        
            var appointementRepository = new AppointementRepository(
                new CSVStream<Appointement>(APPOINTEMENT_FILE, new AppointementCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
                new LongSequencer(),
                doctorRepository,
                patientRepository,
                roomRepository); 
            var secretaryRepository = new SecretaryRepository(
                new CSVStream<Secretary>(SECRETARY_FILE, new SecretaryCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
                new LongSequencer());
            var feedbackRepository = new FeedbackRepository(
                new CSVStream<Feedback>(FEEDBACK_FILE, new FeedbackCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
                new LongSequencer());

            var doctorService = new DoctorService(doctorRepository);
            var patientService = new PatientService(patientRepository);
            var roomService = new RoomService(roomRepository);
            var appointementService = new AppointementService(appointementRepository);
            var secretaryService = new SecretaryService(secretaryRepository);
            var feedbackService = new FeedbackService(feedbackRepository);

            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
            RoomController = new RoomController(roomService);
            AppointementController = new AppointementController(appointementService);
            SecretaryController = new SecretaryController(secretaryService);
            FeedbackController = new FeedbackController(feedbackService);
        }

        public IController<Doctor,long> DoctorController { get; private set; }
        public IPatientController PatientController { get; private set; }
        public IController<Room,long> RoomController { get; private set; }
        public IAppointementController AppointementController { get; private set; }
        public ISecretaryController SecretaryController { get; private set; }
        public IController<Feedback,long> FeedbackController { get; private set; }

        public Doctor TempDoctor { get; set; }
        public Term TempTerm { get; set; }
        public Patient TempPatient { get; set; }
        public Secretary TempSecretary { get; set; }
        public Appointement TempAppointement { get; set; }
        public IEnumerable<Appointement> TempScheduled { get; set; }
        public DateTime TempDateTime { get; set; }
    }
}
