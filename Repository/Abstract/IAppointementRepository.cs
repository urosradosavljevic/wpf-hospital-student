using Bolnica.Model;
using System;
using System.Collections.Generic;

namespace Bolnica.Repository.Abstract
{
    public interface IAppointementRepository : IRepository<Appointement, long>
    {
        IEnumerable<Appointement> GetAppointementsWithoutRoom();
        IEnumerable<Appointement> GetAppointementsByDate(DateTime date);
        IEnumerable<Appointement> GetAppointementsByDoctor(Doctor doc);
    }
}
