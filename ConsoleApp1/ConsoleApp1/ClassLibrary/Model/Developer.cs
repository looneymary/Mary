using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ClassLibrary.Models
{
    public class Developer : Worker
    {
        public string DevLang { get; set; }
        public string Experience { get; set; }
        public string Level { get; set; }
        
        public Developer(string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, string date, 
                                                   int salary, string devLang, string experience, string level) 
                                                   : base(firstName, lastName, sex, appointment, date, salary)
        {
            base.FirstName = firstName;
            base.LastName = lastName;
            base.Sex = sex;
            base.Appointment = appointment;
            base.Date = date;
            base.Salary = salary;
            this.DevLang = devLang;
            this.Experience = experience;
            this.Level = level;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", LastName, FirstName, Sex, Appointment, Date, Salary,
                DevLang, Experience, Level);
        }
    }
}
