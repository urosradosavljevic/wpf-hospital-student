using Bolnica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Controller.Abstract
{
    public interface ISecretaryController:IController<Secretary,long>
    {
        Secretary GetSecretaryByEmail(string email);
    }
}
