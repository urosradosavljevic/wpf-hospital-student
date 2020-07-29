using Bolnica.Controller.Abstract;
using Bolnica.Model;
using Bolnica.Service;
using System.Collections.Generic;

namespace Bolnica.Controller
{
    public class PatientController : IPatientController
    {
        private readonly PatientService service;

        public PatientController(PatientService service)
        {
            this.service = service;
        }

        public IEnumerable<Patient> GetAll() => service.GetAll();

        public Patient GetOne(long id) => service.GetOne(id);

        public Patient Create(Patient patient) => service.Create(patient);

        public void Update(Patient patient) => service.Update(patient);
        
        public void Delete(Patient patient) => service.Delete(patient);

        public IEnumerable<Patient> GetPatientsToPunish() => service.GetPatientsToPunish();
        public IEnumerable<Patient> GetWithoutOneTimePatients() => service.GetWithoutOneTimePatients();
        public Patient GetPatientByEmail(string email) => service.GetPatientByEmail(email);
    }
}
