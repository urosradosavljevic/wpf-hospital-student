using Bolnica.Model.Util;
using Bolnica.Repository.Abstract;
using System;
using System.ComponentModel;

namespace Bolnica.Model
{

    public class User: INotifyPropertyChanged, IIdentifiable<long>
    {
        protected long id;
        protected string firstName;
        protected string lastName;
        protected string fullName;
        protected Email email;
        protected string username;
        protected Password password;
        protected PhoneNumber phoneNumber;
        protected string address;
        protected string gender;
        protected DateTime birthDate;

        public User(long id)
        {
            Id = id;
        }

        public User() { }

        public User(
            long id,
            string firstName, 
            string lastName, 
            Email email, 
            string username, 
            Password password, 
            PhoneNumber phoneNumber, 
            string address, 
            string gender, 
            DateTime birthDate
            )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password; 
            PhoneNumber = phoneNumber;
            Address = address;
            Gender = gender;
            BirthDate = birthDate;
        }

        public User(
            string firstName, 
            string lastName, 
            Email email, 
            string username, 
            Password password, 
            PhoneNumber phoneNumber, 
            string address, 
            string gender, 
            DateTime birthDate
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password; 
            PhoneNumber = phoneNumber;
            Address = address;
            Gender = gender;
            BirthDate = birthDate;
        }

        public User(
            long id,
            string firstName,
            string lastName,
            string gender,
            Email email,
            PhoneNumber phoneNumber,
            string address,
            DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Gender = gender;
            BirthDate = birthDate;
        }
        public User(
            string firstName,
            string lastName,
            string gender,
            Email email,
            PhoneNumber phoneNumber,
            string address,
            DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Gender = gender;
            BirthDate = birthDate;
        }

        #region Getters and setters
        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaiseProperyChanged("Id");
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                RaiseProperyChanged("FirstName");
                RaiseProperyChanged("FullName");
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                RaiseProperyChanged("LastName");
                RaiseProperyChanged("FullName");
            }
        }
        public string FullName
        {
            get
            {
                return firstName +" "+ LastName;
            }
            set
            {
                fullName = value;
                RaiseProperyChanged("FullName");
            }
        }
        public Email Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaiseProperyChanged("Email");
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                RaiseProperyChanged("Username");
            }
        }
        public Password Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaiseProperyChanged("Password");
            }
        }
        public PhoneNumber PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                RaiseProperyChanged("Phone");
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                RaiseProperyChanged("Address");
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                RaiseProperyChanged("Gender");
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
                RaiseProperyChanged("BirthDate");
            }
        }
        public long GetId() => Id;
        public void SetId(long id) => Id = id;


        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseProperyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
