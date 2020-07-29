using Bolnica.Model;
using System.Collections.Generic;

namespace Bolnica.Repository.Abstract
{
    public interface ISecretaryRepository : IRepository<Secretary, long>
    {
        Secretary GetSecretaryByEmail(string username);
    }
}
