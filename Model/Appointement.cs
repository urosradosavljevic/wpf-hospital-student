using Bolnica.Model.Util;
using Bolnica.Repository.Abstract;
using System;
using System.ComponentModel;

namespace Bolnica.Model
{
    public class Appointement : INotifyPropertyChanged, IIdentifiable<long>
    {
        private long id;
        private Doctor doctor;
        private Patient patient;
        private DateTime date;
        private Term term;
        private Room room;
        private string intervention;
        private bool emergency;

        public Appointement (Doctor doctor, Patient patient, DateTime date, Term term, Room room, string intervention,bool emergency) 
        {
            Doctor = doctor;
            Patient = patient;
            Date = date;
            Term = term;
            Room = room;
            Intervention = intervention;
            Emergency = emergency;
        }
        public Appointement (long id,Doctor doctor, Patient patient, DateTime date, Term term, Room room, string intervention,bool emergency) 
        {
            Id = id;
            Doctor = doctor;
            Patient = patient;
            Date = date;
            Term = term;
            Room = room;
            Intervention = intervention;
            Emergency = emergency;
        }
        public Appointement (Doctor doctor, Patient patient, DateTime date, Term term, string intervention, bool emergency) 
        {
            Doctor = doctor;
            Patient = patient;
            Date = date;
            Term = term;
            Intervention = intervention;
            Emergency = emergency;
        }
        public Appointement() { } 

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
        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
                RaiseProperyChanged("Room");
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaiseProperyChanged("Date");
            }
        }
        public Term Term
        {
            get
            {
                return term;
            }
            set
            {
                term = value;
                RaiseProperyChanged("Term");
            }
        }
        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                RaiseProperyChanged("Doctor");
            }
        }
        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                RaiseProperyChanged("Patient");
            }
        }
        public string Intervention
        {
            get
            {
                return intervention;
            }
            set
            {
                intervention = value;
                RaiseProperyChanged("Intervention");
            }
        }
        public bool Emergency
        {
            get
            {
                return emergency;
            }
            set
            {
                emergency = value;
                RaiseProperyChanged("Emergency");
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