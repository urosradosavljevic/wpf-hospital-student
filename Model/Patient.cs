using Bolnica.Model.Util;
using Bolnica.Repository.Abstract;
using System;

namespace Bolnica.Model
{
    public class Patient : User
    {
        private BloodType bloodType;
        private string maritalStatus;
        private string employmentStatus;
        private bool violated;
        private bool punished;
        private bool oneTimePatient;

        #region Contructors

        public Patient() { }
        public Patient(long id) : base(id) {}

        public Patient(
            long id,
            bool punished,
            bool violated,
            bool oneTimePatient,
            string firstName,
            string lastName,
            Email email,
            string username,
            Password password,
            PhoneNumber phoneNumber,
            string address,
            string gender,
            DateTime birthDate) :
            base(id, firstName, lastName, email, username, password, phoneNumber, address, gender, birthDate)
        {
            OneTimePatient = oneTimePatient;
            Punished = punished;
            Violated = violated;
        }
        public Patient(
            bool punished,
            bool violated,
            bool onTimePatient,
            string firstName,
            string lastName,
            Email email,
            string username,
            Password password,
            PhoneNumber phoneNumber,
            string address,
            string gender,
            DateTime birthDate) :
            base(firstName, lastName, email, username, password, phoneNumber, address, gender, birthDate)
        {
            OneTimePatient = onTimePatient;
            Punished = punished;
            Violated = violated;
        }

        #endregion

        #region // Search Combobox ToString setup
        public override string ToString()
        {
            return FullName;
        }
        #endregion

        #region Getters & Setters
        public bool Punished
        { 
            get 
            {
                return punished;
            } 
            set
            {
                punished = value;
                RaiseProperyChanged("Punished");
            } 
        }
        public bool Violated
        { 
            get 
            {
                return violated;
            } 
            set 
            {
                violated = value;
                RaiseProperyChanged("Violated");
            } 
        }
        public bool OneTimePatient
        { 
            get 
            {
                return oneTimePatient;
            } 
            set 
            {
                oneTimePatient = value;
                RaiseProperyChanged("OneTimePatient");
            } 
        }
        public BloodType BloodType
        { 
            get 
            {
                return bloodType;
            } 
            set 
            {
                bloodType = value;
                RaiseProperyChanged("BloodType");
            } 
        }
        public string MaritalStatus
        { 
            get 
            {
                return maritalStatus;
            } 
            set 
            {
                maritalStatus = value;
                RaiseProperyChanged("MaritalStatus");
            } 
        }
        public string EmploymentStatus
        { 
            get 
            {
                return employmentStatus;
            } 
            set 
            {
                employmentStatus = value;
                RaiseProperyChanged("EmploymentStatus");
            }
        }
        #endregion
    }
}
