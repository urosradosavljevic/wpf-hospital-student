using Bolnica.Model.Util;
using Bolnica.Repository.Abstract;
using System;
using System.ComponentModel;

namespace Bolnica.Model
{
    public class Feedback : INotifyPropertyChanged, IIdentifiable<long>
    {
        private long id;
        private DateTime date;
        private string text;

        public Feedback(DateTime date, string text)
        {
            Date = date;
            Text = text;
        }
        public Feedback(long id,DateTime date, string text)
        {
            Id = id;
            Date = date;
            Text = text;
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

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                RaiseProperyChanged("Text");
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