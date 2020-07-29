using Bolnica.Model;
using Bolnica.Repository.Abstract;
using Bolnica.Repository.CSV;
using Bolnica.Repository.CSV.Stream;
using Bolnica.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolnica.Repository
{
    public class DoctorRepository : CSVRepository<Doctor,long>,
        IDoctorRepository,
        IEagerCSVRepository<Doctor,long>
    {

        private const string ENTITY_NAME = "Doctor";

        public DoctorRepository(ICSVStream<Doctor> stream, ISequencer<long> sequencer) : base(ENTITY_NAME,stream,sequencer){}

        public new IEnumerable<Doctor> Find(Func<Doctor, bool> predicate) => GetAllEager().Where(predicate);

        public long GetMaxId(IEnumerable<Doctor> doctors)
        {
            if (doctors.Count() == 0) return 0;
            else return doctors.Max(doc => doc.Id);
        }
        public Doctor GetEager(long id) => GetOne(id);
        public IEnumerable<Doctor> GetAllEager() => GetAll();

    }
}
