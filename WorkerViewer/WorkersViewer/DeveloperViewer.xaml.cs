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
    /// Логика взаимодействия для DeveloperViewer.xaml
    /// </summary>
    public partial class DeveloperViewer : Window
    {
        private readonly Developer _developer;
        private WorkerService _business;
        CheckValidExceptions ex = new CheckValidExceptions();

        string _createOrUpdate;

        public delegate int ValidXmlValuesDelegate(params string[] parametres);
        public event ValidXmlValuesDelegate CheckingValid;

        public DeveloperViewer(WorkerService business, Developer developer, ViewForm form)
        {
            InitializeComponent();
            this._developer = developer ?? new Developer();
            this._business = business;
            this.EditDevForm(developer, form);
            this._createOrUpdate = form.ToString();
        }

        /// <summary>
        /// Fill form of "DeveloperViewer" window
        /// </summary>
        /// <param name="developer"></param>
        public void EditDevForm(Developer developer, ViewForm form)
        {
            if(form == ViewForm.View)
            {
                this.FirstName.IsReadOnly = true;
                this.LastName.IsReadOnly = true;
                this.Appointment.IsReadOnly = true;
                this.Date.IsReadOnly = true;
                this.Salary.IsReadOnly = true;
                this.DevLang.IsReadOnly = true;
                this.Experience.IsReadOnly = true;
                this.Level.IsReadOnly = true;

                this.BtnDevSave.Visibility = Visibility.Hidden;
            }

            this.FirstName.Text = developer.FirstName;
            this.LastName.Text = developer.LastName;
            this.Gender_value.Text = (developer.Sex).ToString();
            this.Appointment.Text = developer.Appointment;
            this.Date.Text = developer.Date;
            this.Salary.Text = developer.Salary.ToString();
            this.DevLang.Text = developer.DevLang;
            this.Experience.Text = developer.Experience;
            this.Level.Text = developer.Level;
        }

        /// <summary>
        /// Click for button "Save" - save changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Btn_Dev_Save(object sender, RoutedEventArgs e)
        {
            CheckingValid += ex.CheckExceptions;

            try
            {
                string firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.FirstName.Text);
                string lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.LastName.Text);
                string sex = this.Gender_value.Text;
                string appointment = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Appointment.Text);
                string date = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Date.Text);
                int salary = int.Parse(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Salary.Text));
                string devLang = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.DevLang.Text);
                string experience = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Experience.Text);
                string level = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Level.Text);

                CheckingValid(firstName, lastName, sex, appointment, date, salary.ToString(), devLang, experience, level);
                if (ex.ValidResult == 0)
                {
                    this._developer.FirstName = firstName;
                    this._developer.LastName = lastName;
                    this._developer.Sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), sex);
                    this._developer.Appointment = appointment;
                    this._developer.Date = date;
                    this._developer.Salary = salary;
                    this._developer.DevLang = devLang;
                    this._developer.Experience = experience;
                    this._developer.Level = level;

                    if (this._createOrUpdate == ViewForm.Create.ToString())
                    {
                        this._business.Create(this._developer);
                        this.Close();
                    }
                    if (this._createOrUpdate == ViewForm.Update.ToString())
                    {
                        this._business.Update(this._developer);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(ex.ExMessage);
                }
            }
            catch
            {
                MessageBox.Show("Invalid value was entered. Please, repeat inpute.\n");
            }
        }

        /// <summary>
        /// Click for button "Close" - close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Dev_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
