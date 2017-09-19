using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ClassLibrary.Models
{
    [Serializable]
    [XmlInclude(typeof(Developer))]
    [XmlInclude(typeof(OfficeWorker))]
    public abstract class Worker
    {
        public Int32 _id;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EnumsForModels.TypeOfSex Sex { get; set; }
        public string Appointment { get; set; }
        public string Date { get; set; }
        public int Salary { get; set; }
        
        public string FullInfo { get; set; }
                        
        // Constructor.
        public Worker()
        {
        }

        public Worker(int id, string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, string date, int salary)
        {
            this._id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Sex = sex;
            this.Appointment = appointment;
            this.Date = date;
            this.Salary = salary;
        }

        /// <summary>
        /// Show properties of worker in one string
        /// </summary>
        /// <returns>The string with properties</returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6}", _id, FirstName, LastName, Sex, Appointment, Date, Salary);
        }
    }
}
