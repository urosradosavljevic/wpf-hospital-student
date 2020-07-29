using Bolnica.Controller.Abstract;
using Bolnica.Model;
using Bolnica.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Controller
{
    class SecretaryController : ISecretaryController
    {
        private readonly SecretaryService service;

        public SecretaryController(SecretaryService service)
        {
            this.service = service;
        }

        public IEnumerable<Secretary> GetAll() => service.GetAll();

        public Secretary GetOne(long id) => service.GetOne(id);

        public Secretary Create(Secretary secretary) => service.Create(secretary);

        public void Update(Secretary secretary) => service.Update(secretary);

        public void Delete(Secretary secretary) => service.Delete(secretary);

        public Secretary GetSecretaryByEmail(string email) => service.GetSecretaryByEmail(email);
    }
}
