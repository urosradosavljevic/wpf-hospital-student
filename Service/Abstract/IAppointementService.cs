using Bolnica.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public interface IAppointementService : IService<Appointement, long>
    {
        IEnumerable<Appointement> GetAppointementsWithoutRoom();
        IEnumerable<Appointement> GetAppointementsByDate(DateTime date);
        IEnumerable<Appointement> GetAppointementsByDoctor(Doctor doctor);
    }
}
