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
    public class SecretaryRepository : CSVRepository<Secretary,long>,
        ISecretaryRepository,
        IEagerCSVRepository<Secretary,long>
    {

        private const string ENTITY_NAME = "Secretary";

        public SecretaryRepository(ICSVStream<Secretary> stream, ISequencer<long> sequencer) : base(ENTITY_NAME,stream,sequencer) {}

        public new IEnumerable<Secretary> Find(Func<Secretary, bool> predicate) => GetAllEager().Where(predicate);

        public long GetMaxId(IEnumerable<Secretary> secretaries)
        {
            if (secretaries.Count() == 0) return 0;
            else return secretaries.Max(doc => doc.Id);
        }
        public Secretary GetEager(long id) => GetOne(id);
        public Secretary GetSecretaryByEmail(string email)
        {
            var secretaries = Find(secretary => secretary.Email.Value == email);
            if (secretaries.ToList().Count() == 0)
            {
                return null;
            }
            return secretaries.First();
        }

        public IEnumerable<Secretary> GetAllEager() => GetAll();

    }
}
