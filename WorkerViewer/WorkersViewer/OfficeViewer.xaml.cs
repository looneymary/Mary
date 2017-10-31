using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess.Models;
using DataAccess;
using BusinessLayer;
using WorkerViewer;
using System.Globalization;

namespace WorkersViewer
{
    /// <summary>
    /// Логика взаимодействия для OfficeViewer.xaml
    /// </summary>
    public partial class OfficeViewer : Window
    {
        private readonly OfficeWorker _office = new OfficeWorker();
        private WorkerService _business;
        CheckValidExceptions ex = new CheckValidExceptions();
        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        MainWindow.ViewForm windowForm;

        public delegate int ValidValuesDelegate(params string[] paarametres);
        public event ValidValuesDelegate CheckingValid;

        public OfficeViewer(OfficeWorker office, MainWindow.ViewForm form, string xmlFile)
        {
            InitializeComponent();
            this._office = office;
            this._business = new WorkerService(new XmlRepository(), xmlFile);
            windowForm = form;
            this.EditOfficeForm(this._office);
        }

        /// <summary>
        /// Fill form of "OfficeViewer" window
        /// </summary>
        /// <param name="office">Object to edit</param>
        public void EditOfficeForm(OfficeWorker office)
        {
            if (windowForm == MainWindow.ViewForm.View)
            {
                this.FirstName.IsReadOnly = true;
                this.LastName.IsReadOnly = true;
                this.Appointment.IsReadOnly = true;
                this.Date.IsReadOnly = true;
                this.Salary.IsReadOnly = true;
                this.YearsInService.IsReadOnly = true;

                this.BtnOfficeSave.Visibility = Visibility.Hidden;
            }

            this.FirstName.Text = this._office.FirstName;
            this.LastName.Text = this._office.LastName;
            this.Gender_value.Text = this._office.Sex.ToString();
            this.Appointment.Text = this._office.Appointment;
            this.Date.Text = this._office.Date;
            this.Salary.Text = this._office.Salary.ToString();
            this.YearsInService.Text = this._office.YearsInService.ToString();
        }

        /// <summary>
        /// Click for button "Save" - save changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Btn_Office_Save(object sender, RoutedEventArgs e)
        {
            CheckingValid += ex.CheckExceptions;

            try
            {
                string firstName = ti.ToTitleCase(this.FirstName.Text);
                string lastName = ti.ToTitleCase(this.LastName.Text);
                string sex = this.Gender_value.Text;
                string appointment = ti.ToTitleCase(this.Appointment.Text);
                string date = ti.ToTitleCase(this.Date.Text);
                int salary = int.Parse(ti.ToTitleCase(this.Salary.Text));
                int yearsInOService = int.Parse(ti.ToTitleCase(this.YearsInService.Text));

                MessageBox.Show("Validation of data.");

                CheckingValid(firstName, lastName, sex, appointment, date, salary.ToString(), yearsInOService.ToString());

                if (ex.ValidResult == 0)
                {
                    this._office.FirstName = firstName;
                    this._office.LastName = lastName;
                    this._office.Sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), sex);
                    this._office.Appointment = appointment;
                    this._office.Date = date;
                    this._office.Salary = salary;
                    this._office.YearsInService = yearsInOService;

                    if(windowForm == MainWindow.ViewForm.Create) this._business.Create(this._office);                    
                    if(windowForm == MainWindow.ViewForm.Update) this._business.Update(this._office);

                    this.Close();
                }
                else
                {
                    MessageBox.Show(ex.ExMessage);
                }
            }
            catch
            {
                MessageBox.Show("Invalid value was entered. Please, repeat inpute.");
            }
        }

        /// <summary>
        /// Click for button "Close" - close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Btn_Office_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
