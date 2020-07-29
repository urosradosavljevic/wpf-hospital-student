using Bolnica.Model;
using Bolnica.Repository.Abstract;
using Bolnica.Repository.CSV;
using Bolnica.Repository.CSV.Stream;
using Bolnica.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolnica.Repository
{
    public class AppointementRepository : CSVRepository<Appointement, long>,
        IAppointementRepository,
        IEagerCSVRepository<Appointement, long>
    {

        private const string ENTITY_NAME = "Appointement";
        private readonly IEagerCSVRepository<Doctor, long> _doctorRepository;
        private readonly IEagerCSVRepository<Patient, long> _patientRepository;
        private readonly IEagerCSVRepository<Room, long> _roomRepository;

        public AppointementRepository(ICSVStream<Appointement> stream,
            ISequencer<long> sequencer,
            IEagerCSVRepository<Doctor, long> doctorRepository,
            IEagerCSVRepository<Patient, long> patientRepository,
            IEagerCSVRepository<Room, long> roomRepository
            ) : base(ENTITY_NAME, stream, sequencer)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _roomRepository = roomRepository;
        }

        public new IEnumerable<Appointement> Find(Func<Appointement, bool> predicate) => GetAllEager().Where(predicate);

        public Appointement GetEager(long id)
        {
            var appointement = GetOne(id);
            appointement.Doctor = _doctorRepository.GetEager(appointement.Doctor.Id);
            appointement.Patient = _patientRepository.GetEager(appointement.Patient.Id);
            appointement.Room = _roomRepository.GetEager(appointement.Room.Id);
            return appointement;
        }

        public IEnumerable<Appointement> GetAllEager()
        {
            var appointements = GetAll();
            var doctors = _doctorRepository.GetAllEager();
            var patients = _patientRepository.GetAllEager();
            var rooms = _roomRepository.GetAllEager();
            BindDoctorsWithAppointements(doctors, appointements);
            BindPatientsWithAppointements(patients, appointements);
            BindRoomsWithAppointements(rooms, appointements);
            return appointements;
        }
        public IEnumerable<Appointement> GetAppointementsWithoutRoom() => Find(app => app.Room.RoomNumber == 0);
        public IEnumerable<Appointement> GetAppointementsByDoctor(Doctor doc) => Find(app => app.Doctor.Id == doc.Id);

        public IEnumerable<Appointement> GetAppointementsByDate(DateTime date)
        {
            var apps = Find(app => app.Date.CompareTo(date) == 0 ? true : false);
            return apps;
        }
        private void BindDoctorsWithAppointements(IEnumerable<Doctor> doctors, IEnumerable<Appointement> appointements)
           => appointements
           .ToList()
           .ForEach(appointement => appointement.Doctor = GetDoctorById(doctors, appointement.Doctor.Id));

        private Doctor GetDoctorById(IEnumerable<Doctor> doctors, long id) => doctors.SingleOrDefault(doctor => doctor.Id == id);

        private void BindPatientsWithAppointements(IEnumerable<Patient> patients, IEnumerable<Appointement> appointements)
           => appointements
           .ToList()
           .ForEach(appointement => appointement.Patient = GetPatientById(patients, appointement.Patient.Id));

        private Patient GetPatientById(IEnumerable<Patient> patients, long id) => patients.SingleOrDefault(patient => patient.Id == id);

        private void BindRoomsWithAppointements(IEnumerable<Room> rooms, IEnumerable<Appointement> appointements)
           => appointements
           .ToList()
           .ForEach(appointement => appointement.Room = GetRoomById(rooms, appointement.Room.Id));

        private Room GetRoomById(IEnumerable<Room> rooms, long id) => rooms.SingleOrDefault(room => room.Id == id);

    }
}
