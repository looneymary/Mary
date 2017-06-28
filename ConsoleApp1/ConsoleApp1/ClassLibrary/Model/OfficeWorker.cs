using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class OfficeWorker : Worker
    {
        public OfficeWorker(string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, 
                            string date, int salary) : base(firstName, lastName, sex, appointment, date, salary)
        {
            base.FirstName = firstName;
            base.LastName = lastName;
            base.Sex = sex;
            base.Appointment = appointment;
            base.Date = date;
            base.Salary = salary;
        }
    }
}
