using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Repository.Abstract
{
    public interface IIdentifiable<T>
    {
        T GetId();
        void SetId(T id);
    }
}
