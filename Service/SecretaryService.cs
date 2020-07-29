using Bolnica.Model;
using Bolnica.Repository;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class SecretaryService : ISecretaryService
    {

        private readonly SecretaryRepository repository;

        public SecretaryService(SecretaryRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Secretary> GetAll() => repository.GetAll();

        public Secretary GetOne(long id) => repository.GetOne(id);

        public Secretary Create(Secretary secretary) => repository.Create(secretary);

        public void Update(Secretary secretary) => repository.Update(secretary);

        public void Delete(Secretary secretary) => repository.Delete(secretary);

        public Secretary GetSecretaryByEmail(string username) => repository.GetSecretaryByEmail(username);
        /*

        public Secretary[] ViewAvailableSecretarys(DateTime start, DateTime end)
        {
            // TODO: implement+
            return null;
        }

        public int ViewAvailableDates(int secretaryId)
        {
            // TODO: implement+
            return 0;
        }

        public DateTime[] ViewAvaliableDates(int secretaryID)
        {
            // TODO: implement +
            return null;
        }

        public DateTime[] ViewAvaliableTerms(int secretaryID, DateTime date)
        {
            // TODO: implement +
            return null;
        }
        */
    }
}
