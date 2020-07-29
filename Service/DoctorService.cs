using Bolnica.Model;
using Bolnica.Repository;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class DoctorService : IService<Doctor,long>
    {

        private readonly DoctorRepository _repository;

        public DoctorService(DoctorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Doctor> GetAll() => _repository.GetAll();

        public Doctor GetOne(long id) => _repository.GetOne(id);

        public Doctor Create(Doctor doctor) => _repository.Create(doctor);

        public void Update(Doctor doctor) => _repository.Update(doctor);

        public void Delete(Doctor doctor) => _repository.Delete(doctor);

        /*

        public Doctor[] ViewAvailableDoctors(DateTime start, DateTime end)
        {
            // TODO: implement+
            return null;
        }

        public int ViewAvailableDates(int doctorId)
        {
            // TODO: implement+
            return 0;
        }

        public DateTime[] ViewAvaliableDates(int doctorID)
        {
            // TODO: implement +
            return null;
        }

        public DateTime[] ViewAvaliableTerms(int doctorID, DateTime date)
        {
            // TODO: implement +
            return null;
        }
        */
    }
}
