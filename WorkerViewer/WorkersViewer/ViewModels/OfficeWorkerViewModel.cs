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
    class OfficeWorkerViewModel : BaseWorkerViewModel
    {
        public delegate int ValidValuesDelegate(params string[] paarametres);
        public event ValidValuesDelegate CheckingValid;

        #region Fields
        private WorkerService _business;
        private CheckValidExceptions _ex;
        private int _preYerasInservice;
        private int _yearsInService;
        #endregion

        public int YearsInService
        {
            get { return this._yearsInService; }
            set
            {
                this._yearsInService = value;
                this._ex = new CheckValidExceptions();
                RaisePropertyChanged(nameof(YearsInService));
            }
        }

        public OfficeWorkerViewModel()
        {
            this._business = new WorkerService(new XmlRepository());
            this._ex = new CheckValidExceptions();
        }

        public OfficeWorkerViewModel(string xmlFile)
        {
            this._business = new WorkerService(new XmlRepository(xmlFile), xmlFile);
            this._ex = new CheckValidExceptions();
        }

        public ICommand SaveCommand => new CommandHandlerGeneric<Window>(Save, true);

        #region Methods
        /// <summary>
        /// Save new worker or changed properties of worker
        /// </summary>
        /// <param name="window">The window where the worker changes</param>
        private void Save(Window window)
        {
            if (base._xmlOpenFile != null) this._business = new WorkerService(new XmlRepository(base._xmlOpenFile), base._xmlOpenFile);
            if (CreateorUpdate == CreateOrUpdate.Create)
            {
                this.Edit(window, new OfficeWorker());
            }
            if(CreateorUpdate == CreateOrUpdate.Update)
            {
                this.Edit(window, (OfficeWorker)Mapper.MapModelToEntity(SelectedWorker));
            }
        }

        /// <summary>
        /// Save the original values of the object
        /// </summary>
        public override void BeginEdit()
        {
            base.BeginEdit();
            this._preYerasInservice = this.YearsInService;
        }

        /// <summary>
        /// Edit worker or add new worker
        /// </summary>
        /// <param name="window">Window where the worker changes</param>
        /// <param name="office">Object to edit</param>
        public void Edit(Window window, OfficeWorker office)
        {
            CheckingValid += this._ex.CheckExceptions;
            //try
            //{
                string firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.FirstName);
                string lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.LastName);
                string sex = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Sex.ToString());
                string appointment = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Appointment);
                string date = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Date);
                int salary = this.Salary;
                int yearsInOService = this.YearsInService;
                this.Type = "Office worker";

                CheckingValid(firstName, lastName, sex, appointment, date, salary.ToString(), yearsInOService.ToString());

                if (this._ex.ValidResult == 0)
                {
                    office.FirstName = firstName;
                    office.LastName = lastName;
                    office.Sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), sex);
                    office.Appointment = appointment;
                    office.Date = date;
                    office.Salary = salary;
                    office.YearsInService = yearsInOService;

                    if(CreateorUpdate == CreateOrUpdate.Create) this._business.Create(office);
                    if(CreateorUpdate == CreateOrUpdate.Update) this._business.Update(office);
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
            this.YearsInService = this._preYerasInservice;
        }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="window">Window "Developer"</param>
        protected override void Close(Window window)
        {
            // For method "Update" to save original values 
            if (CreateorUpdate == CreateOrUpdate.Update) CancelEdit();
            base.Close(window);            
        }
        #endregion
    }
}
