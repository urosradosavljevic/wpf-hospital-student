using Bolnica.Controller.Abstract;
using Bolnica.Model;
using Bolnica.Service;
using System;
using System.Collections.Generic;

namespace Bolnica.Controller
{
    public class AppointementController: IAppointementController
    {
        private readonly AppointementService service;

        public AppointementController(AppointementService service)
        {
            this.service = service;
        }

        public IEnumerable<Appointement> GetAll() => service.GetAll();

        public IEnumerable<Appointement> GetAppointementsWithoutRoom() => service.GetAppointementsWithoutRoom();
        public IEnumerable<Appointement> GetAppointementsByDate(DateTime date) => service.GetAppointementsByDate(date);
        public IEnumerable<Appointement> GetAppointementsByDoctor(Doctor date) => service.GetAppointementsByDoctor(date);

        public Appointement GetOne(long id) => service.GetOne(id);

        public Appointement Create(Appointement appointement) => service.Create(appointement);

        public void Update(Appointement appointement) => service.Update(appointement);

        public void Delete(Appointement appointement) => service.Delete(appointement);
    }
}
