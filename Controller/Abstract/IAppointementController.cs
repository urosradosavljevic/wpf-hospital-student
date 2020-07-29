using Bolnica.Model;
using System;
using System.Collections.Generic;

namespace Bolnica.Controller.Abstract
{
    public interface IAppointementController: IController<Appointement,long>
    {
        IEnumerable<Appointement> GetAppointementsWithoutRoom();
        IEnumerable<Appointement> GetAppointementsByDate(DateTime date);
        IEnumerable<Appointement> GetAppointementsByDoctor(Doctor doctor);
    }
}
