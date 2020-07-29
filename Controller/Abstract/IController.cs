using System;
using System.Collections.Generic;

namespace Bolnica.Controller
{
    public interface IController<E,ID> where E : class
    {
        IEnumerable<E> GetAll();

        E GetOne(long id);

        E Create(E entity);

        void Update(E entity);

        void Delete(E entity);
    }
}
