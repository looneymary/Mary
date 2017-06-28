using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ClassLibrary.Models
{
    public abstract class Worker
    {
        public Guid _id;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EnumsForModels.TypeOfSex Sex { get; set; }
        public string Appointment { get; set; }
        public string Date { get; set; }
        public int Salary { get; set; }
        
        public string FullInfo { get; set; }
                        
        // Конструктор.
        public Worker(string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, string date, int salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Sex = sex;
            this.Appointment = appointment;
            this.Date = date;
            this.Salary = salary;
        }

        // Переопределения метода ToString.
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", LastName, FirstName, Sex, Appointment, Date, Salary);
        }
    }
}
