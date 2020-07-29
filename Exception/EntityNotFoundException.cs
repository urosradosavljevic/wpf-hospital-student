using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Exception
{
    class EntityNotFoundException : System.Exception
    {
        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(string message) : base(message)
        {

        }

        public EntityNotFoundException(string message, System.Exception inner) : base(message, inner)
        {

        }
    }
}
