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
        private BusinessLayerMethods _business;
        CheckValidExceptions ex = new CheckValidExceptions();
        MainWindow.ViewForm windowForm;

        public delegate int ValidXmlValuesDelegate(params string[] parametres);
        public event ValidXmlValuesDelegate CheckingValid;
        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

        public DeveloperViewer(Developer developer, MainWindow.ViewForm form)
        {
            InitializeComponent();
            this._developer = developer ?? new Developer();
            this._business = new BusinessLayerMethods(new XmlRepository());
            windowForm = form;
            this.EditDevForm(developer);
        }

        public void EditDevForm(Developer developer)
        {
            if(windowForm == MainWindow.ViewForm.View)
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

        public void Btn_Dev_Save(object sender, RoutedEventArgs e)
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
                string devLang = ti.ToTitleCase(this.DevLang.Text);
                string experience = ti.ToTitleCase(this.Experience.Text);
                string level = ti.ToTitleCase(this.Level.Text);

                MessageBox.Show("Validation of data.");

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

                    if(windowForm == MainWindow.ViewForm.Create)
                    {
                        this._business.Create(this._developer);
                        this.Close();
                    }
                    if (windowForm == MainWindow.ViewForm.Update)
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

        private void Btn_Dev_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
