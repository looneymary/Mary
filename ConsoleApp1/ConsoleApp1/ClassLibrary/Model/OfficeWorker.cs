using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    [Serializable]
    public class OfficeWorker : Worker
    {
        public int YearsInService {get; set;}

        public OfficeWorker()
        {

        }

        public OfficeWorker(int id, string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, 
                            string date, int salary, int yearsInService) : base(id, firstName, lastName, sex, appointment, date, salary)
        {
            base._id = id;
            base.FirstName = firstName;
            base.LastName = lastName;
            base.Sex = sex;
            base.Appointment = appointment;
            base.Date = date;
            base.Salary = salary;
            this.YearsInService = yearsInService;
        }

        /// <summary>
        /// Show all properties of worker in one string
        /// </summary>
        /// <returns>The string, contains properties of worker</returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", _id, FirstName, LastName, Sex, Appointment, Date, Salary, YearsInService);
        }
    }
}
