using Bolnica.Model.Util;
using Bolnica.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Model
{
    public class Secretary : User
    {

        public Secretary(long id) : base(id) {}

        public Secretary(
            long id,
            string firstName,
            string lastName,
            Email email,
            string username,
            Password password,
            PhoneNumber phoneNumber,
            string address,
            string gender,
            DateTime birthDate) : 
            base(id,firstName,lastName,email,username,password,phoneNumber,address,gender,birthDate)
        {}
        public Secretary(
            string firstName,
            string lastName,
            Email email,
            string username,
            Password password,
            PhoneNumber phoneNumber,
            string address,
            string gender,
            DateTime birthDate) : 
            base(firstName,lastName,email,username,password,phoneNumber,address,gender,birthDate)
        {}


    }
}
