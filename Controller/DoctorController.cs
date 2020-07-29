using Bolnica.Model;
using Bolnica.Service;
using System.Collections.Generic;

namespace Bolnica.Controller
{
    public class DoctorController : IController<Doctor,long>
    {
        private readonly DoctorService service;

        public DoctorController(DoctorService service)
        {
            this.service = service;
        }

        public IEnumerable<Doctor> GetAll() => service.GetAll();

        public Doctor GetOne(long id) => service.GetOne(id);

        public Doctor Create(Doctor doctor) => service.Create(doctor);

        public void Update(Doctor doctor) => service.Update(doctor);

        public void Delete(Doctor doctor) => service.Delete(doctor);
    }
}
