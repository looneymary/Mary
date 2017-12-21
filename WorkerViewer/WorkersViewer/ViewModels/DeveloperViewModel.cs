using BusinessLayer;
using DataAccess;
using DataAccess.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using WorkersViewer.Properties;
using WorkerViewer.Infrastructure;

namespace WorkerViewer.ViewModels
{
    class DeveloperViewModel : BaseWorkerViewModel
    {
        public delegate int ValidValuesDelegate(params string[] paarametres);
        public event ValidValuesDelegate CheckingValid;
        
        private WorkerService _business;
        private CheckValidExceptions _ex;

        #region Fields
        private string _preDevLang;
        private string _preExperience;
        private string _preLevel;

        private string _devLang;
        private string _experience;
        private string _level;
        #endregion

        #region Properties
        public string DevLang
        {
            get { return this._devLang; }
            set
            {
                this._devLang = value;
                RaisePropertyChanged(nameof(this.DevLang));
            }
        }
        public string Experience
        {
            get { return this._experience; }
            set
            {
                this._experience = value;
                RaisePropertyChanged(nameof(Experience));
            }
        }
        public string Level
        {
            get { return this._level; }
            set
            {
                this._level = value;
                RaisePropertyChanged(nameof(Level));
            }
        }
        #endregion

        public DeveloperViewModel()
        {
            this._business = new WorkerService(new XmlRepository());
            this._ex = new CheckValidExceptions();
        }

        public DeveloperViewModel(string xmlFile)
        {
            this._business = new WorkerService(new XmlRepository(xmlFile), xmlFile);
            this._ex = new CheckValidExceptions();
        }

        public ICommand SaveCommand => new CommandHandlerGeneric<Window>(Save, true);

        #region Methods
        /// <summary>
        /// Save the original values of the object
        /// </summary>
        public override void BeginEdit()
        {
            base.BeginEdit();
            this._preDevLang = this.DevLang;
            this._preExperience = this.Experience;
            this._preLevel = this.Level;            
        }

        /// <summary>
        /// Save new worker or changed properties of worker
        /// </summary>
        /// <param name="window">The window where the worker changes</param>
        private void Save(Window window)
        {
            if (base._xmlOpenFile != null) this._business = new WorkerService(new XmlRepository(), base._xmlOpenFile);
            if (CreateorUpdate == CreateOrUpdate.Create)
            {
                this.Edit(window, new Developer());
            }
            if(CreateorUpdate == CreateOrUpdate.Update)
            {
                this.Edit(window, (Developer)Mapper.MapModelToEntity(SelectedWorker));
            }
        }

        /// <summary>
        /// Edit worker or add new worker
        /// </summary>
        /// <param name="window">Window where the worker changes</param>
        /// <param name="dev">Object to edit</param>
        public void Edit(Window window, Developer dev)
        {
            CheckingValid += this._ex.CheckExceptions;
            //try
            //{
                string firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.FirstName);
                string lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.LastName);
                string sex = this.Sex.ToString();
                string appointment = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Appointment);
                string date = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Date);
                int salary = this.Salary;
                string devLang = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.DevLang);
                string experience = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Experience);
                string level = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Level);
                this.Type = "Developer";

                CheckingValid(firstName, lastName, sex, appointment, date, salary.ToString(), devLang, experience, level);

                if (this._ex.ValidResult == 0)
                {
                    dev.FirstName = firstName;
                    dev.LastName = lastName;
                    dev.Sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), sex);
                    dev.Appointment = appointment;
                    dev.Date = date;
                    dev.Salary = salary;
                    dev.DevLang = DevLang;
                    dev.Experience = experience;
                    dev.Level = level;

                    if (CreateorUpdate == CreateOrUpdate.Create) this._business.Create(dev);
                    if (CreateorUpdate == CreateOrUpdate.Update) this._business.Update(dev);
                    base.IsCreate = true;
                    window.Close();
                }
                else
                {
                    MessageBox.Show(this._ex.ExMessage);
                }
            //}
            //catch
            //{
            //    MessageBox.Show(Resources.EditErrorMessage);
            //}
        }

        /// <summary>
        /// Return the original values to the worker's properties
        /// </summary>
        public override void CancelEdit()
        {
            base.CancelEdit();
            this.DevLang = this._preDevLang;
            this.Experience = this._preExperience;
            this.Level = this._preLevel;
        }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="window">Window "Office"</param>
        protected override void Close(Window window)
        {
            // For method "Update" to save original values 
            if (CreateorUpdate == CreateOrUpdate.Update) this.CancelEdit();
            base.Close(window);
        }
        #endregion
    }
}
