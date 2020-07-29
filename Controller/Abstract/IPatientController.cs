using Bolnica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Controller.Abstract
{
    public interface IPatientController:IController<Patient,long>
    {
        IEnumerable<Patient> GetPatientsToPunish();
        Patient GetPatientByEmail(string email);

    }
}
