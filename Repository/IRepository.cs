using Bolnica.Repository.Abstract;
using System;
using System.Collections.Generic;

namespace Bolnica.Repository
{
    public interface IRepository<E,ID> 
        where E: IIdentifiable<ID>
        where ID: IComparable

    {
        E GetOne(ID id);
        IEnumerable<E> GetAll();
        E Create(E entity);
        void Update(E entity);
        void Delete(E entity);
    }
}
