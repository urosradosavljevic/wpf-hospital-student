using Bolnica.Model;
using System.Collections.Generic;

namespace Bolnica.Service.Abstract
{
    interface IPatientService: IService<Patient,long>
    {
        IEnumerable<Patient> GetPatientsToPunish();
        Patient GetPatientByEmail(string email);
        IEnumerable<Patient> GetWithoutOneTimePatients();
    }
}
