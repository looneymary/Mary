using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Serializable]
    public class OfficeWorker : Worker
    {
        public int YearsInService {get; set;}

        public OfficeWorker()
        {
            base._id = Guid.NewGuid();
        }

        public OfficeWorker(string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, 
                            string date, int salary, int yearsInService) : base(firstName, lastName, sex, appointment, date, salary)
        {
            base._id = Guid.NewGuid();
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
            return String.Format("{0} {1}, {2}, {3}, work since {4}, salary: {5}, working for {6} year(s) ", FirstName, LastName, Sex.ToString().ToLower(), Appointment.ToLower(), Date, Salary, YearsInService);
        }
    }
}
