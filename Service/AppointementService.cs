using Bolnica.Model;
using Bolnica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolnica.Service
{
    public class AppointementService : IAppointementService
    {

        private readonly AppointementRepository _repository;

        public AppointementService(AppointementRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Appointement> GetAppointementsWithoutRoom() => _repository.GetAppointementsWithoutRoom();
        public IEnumerable<Appointement> GetAppointementsByDate(DateTime date) => _repository.GetAppointementsByDate(date);
        public IEnumerable<Appointement> GetAppointementsByDoctor(Doctor date) => _repository.GetAppointementsByDoctor(date);

        public IEnumerable<Appointement> GetAll() => _repository.GetAllEager();

        public Appointement Create(Appointement appointement) => _repository.Create(appointement);

        public void Delete(Appointement apointement) => _repository.Delete(apointement);

        public Appointement GetOne(long id) => _repository.GetEager(id);

        public void Update(Appointement apointement) => _repository.Update(apointement);

        IEnumerable<Appointement> IService<Appointement, long>.GetAll() => _repository.GetAll();
    }
}
