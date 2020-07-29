using Bolnica.Model;
using Bolnica.Repository;
using Bolnica.Service.Abstract;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class PatientService : IPatientService
    {

        private readonly PatientRepository repository;

        public PatientService(PatientRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Patient> GetAll() => repository.GetAll();

        public Patient GetOne(long id) => repository.GetOne(id);

        public Patient Create(Patient patient) => repository.Create(patient);

        public void Update(Patient patient) => repository.Update(patient);

        public void Delete(Patient patient) => repository.Delete(patient);

        public IEnumerable<Patient> GetPatientsToPunish() => repository.GetPatientsToPunish();
        public IEnumerable<Patient> GetWithoutOneTimePatients() => repository.GetWithoutOneTimePatients();
        public Patient GetPatientByEmail(string email) => repository.GetPatientByEmail(email);

        /*

        public Patient[] ViewAvailablePatients(DateTime start, DateTime end)
        {
            // TODO: implement+
            return null;
        }

        public int ViewAvailableDates(int patientId)
        {
            // TODO: implement+
            return 0;
        }

        public DateTime[] ViewAvaliableDates(int patientID)
        {
            // TODO: implement +
            return null;
        }

        public DateTime[] ViewAvaliableTerms(int patientID, DateTime date)
        {
            // TODO: implement +
            return null;
        }
        */
    }
}
