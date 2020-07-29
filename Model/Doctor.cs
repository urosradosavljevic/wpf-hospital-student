using Bolnica.Model.Util;
using Bolnica.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Model
{
    public class Doctor : User
    {
        private Rate rating;

        public Doctor(long id) : base(id) {}

        public Doctor(
            long id,
            Rate rating,
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
        {
            Rating = rating;
        }
        public Doctor(
            Rate rating,
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
        {
            Rating = rating;
        }

        public Rate Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                RaiseProperyChanged("Rating");
            }
        }

    }
}
