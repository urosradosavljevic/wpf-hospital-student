using Bolnica.Model;
using System.Collections;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public interface ISecretaryService : IService<Secretary, long>
    {
        Secretary GetSecretaryByEmail(string email);
    }
}
