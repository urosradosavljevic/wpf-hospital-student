using Bolnica.Model;
using System.Collections;
using System.Collections.Generic;

namespace Bolnica.Repository.Abstract
{
    public interface IPatientRepository : IRepository<Patient, long>
    {
        IEnumerable<Patient> GetPatientsToPunish();
        IEnumerable<Patient> GetWithoutOneTimePatients();
        Patient GetPatientByEmail(string email);
    }
}
