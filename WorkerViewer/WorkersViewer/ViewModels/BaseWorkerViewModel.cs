using DataAccess;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using WorkerViewer.Infrastructure;

namespace WorkerViewer.ViewModels
{
    public enum CreateOrUpdate {Create = 0, Update = 1}

    public abstract class BaseWorkerViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private EnumsForModels.TypeOfSex _sex;
        private string _appointment;
        private string _date;
        private int _salary;
        private string _type;

        // To chose can user update values for windows "Office" & "Developer"
        private bool _isReadOnly;            

        // Fields to save the original values
        protected string _preFirstName;
        protected string _preLastName;
        protected EnumsForModels.TypeOfSex _preSex;
        protected string _preAppointment;
        protected string _preDate;
        protected int _preSalary;
        protected string _preType;
        #endregion

        #region Properties
        public Guid Id
        {
            get { return this._id; }
            set
            {
                this._id = value;
                RaisePropertyChanged(nameof(this.Id));
            }
        }
        public string FirstName
        {
            get { return this._firstName; }
            set
            {
                this._firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                RaisePropertyChanged(nameof(this.FirstName));
            }
        }
        public string LastName
        {
            get { return this._lastName; }
            set
            {
                this._lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                RaisePropertyChanged(nameof(this.LastName));
            }
        }
        public EnumsForModels.TypeOfSex Sex
        {
            get { return this._sex; }
            set
            {
                this._sex = value;
                RaisePropertyChanged(nameof(this.Sex));
            }
        }
        public string Appointment
        {
            get { return this._appointment; }
            set
            {
                this._appointment = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                RaisePropertyChanged(nameof(this.Appointment));
            }
        }
        public string Date
        {
            get { return this._date; }
            set
            {
                this._date = value;
                RaisePropertyChanged(nameof(this.Date));
            }
        }
        public int Salary
        {
            get { return this._salary; }
            set
            {
                this._salary = value;
                RaisePropertyChanged(nameof(Salary));
            }
        }
        public string Type
        {
            get { return this._type; }
            set
            {
                this._type = value;
                RaisePropertyChanged(nameof(this.Type));
            }
        }

        // Property to send worker for updating
        public BaseWorkerViewModel SelectedWorker { get; set; }
        // To save the path of open xml-document
        public string _xmlOpenFile;
        public bool IsReadOnly
        {
            get { return this._isReadOnly; }
            set
            {
                this._isReadOnly = value;
                RaisePropertyChanged(nameof(this.IsReadOnly));
            }
        }
        // To notify that the worker was created
        public bool IsCreate { get; set; } = false;
        // To chose the operation
        public CreateOrUpdate CreateorUpdate { get; set; }
        #endregion

        /// <summary>
        /// Save original values of the object
        /// </summary>
        public virtual void BeginEdit()
        {
            this._preFirstName = this.FirstName;
            this._preLastName = this.LastName;
            this._preSex = this.Sex;
            this._preAppointment = this.Appointment;
            this._preDate = this.Date;
            this._preSalary = this.Salary;
        }

        /// <summary>
        /// Return original values to the object
        /// </summary>
        public virtual void CancelEdit()
        {
            this.FirstName = this._preFirstName;
            this.LastName = this._preLastName;
            this.Sex = this._preSex;
            this.Appointment = this._preAppointment;
            this.Date = this._preDate;
            this.Salary = this._preSalary;
        }
        
        public ICommand CloseCommand => new CommandHandlerGeneric<Window>(Close, true);

        /// <summary>
        /// Close the window (for element of menu "Close")
        /// </summary>
        /// <param name="window">Window that need to close</param>
        protected virtual void Close(Window window)
        {
            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
    }
}
