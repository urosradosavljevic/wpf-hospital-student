using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Service
{
    public interface IService<E,ID> where E : class
    {
        IEnumerable<E> GetAll();

        E GetOne(long id);

        E Create(E doctor);

        void Update(E doctor);

        void Delete(E doctor);

    }
}
