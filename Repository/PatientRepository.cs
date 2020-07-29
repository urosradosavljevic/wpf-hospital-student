using Bolnica.Exception;
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
    public class PatientRepository: CSVRepository<Patient,long>,
        IPatientRepository,
        IEagerCSVRepository<Patient,long>
    {
        private const string ENTITY_NAME = "Patient";

        public new IEnumerable<Patient> Find(Func<Patient, bool> predicate) => GetAllEager().Where(predicate);

        public PatientRepository(ICSVStream<Patient> stream, ISequencer<long> sequencer) : base(ENTITY_NAME,stream,sequencer) {}

        public Patient GetEager(long id) => GetOne(id);

        public IEnumerable<Patient> GetAllEager() => GetAll();
        public IEnumerable<Patient> GetWithoutOneTimePatients() => Find(p => p.OneTimePatient = false);
        public IEnumerable<Patient> GetPatientsToPunish() => Find(p => p.Violated);
        public Patient GetPatientByEmail(string email)
        {
            var patients = Find(secretary => secretary.Email.Value == email);
            if (patients.ToList().Count() == 0)
            {
                return null;
            }
            return patients.First();
        }
    }
}
