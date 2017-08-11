using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class OfficeWorker : Worker
    {
        public OfficeWorker(int id, string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, 
                            string date, int salary) : base(id, firstName, lastName, sex, appointment, date, salary)
        {
            base._id = id;
            base.FirstName = firstName;
            base.LastName = lastName;
            base.Sex = sex;
            base.Appointment = appointment;
            base.Date = date;
            base.Salary = salary;
        }
    }
}
