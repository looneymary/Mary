using System;

namespace DataAccess.Models
{
    [Serializable]    
    public class Developer : Worker
    {
        public string DevLang { get; set; }
        public string Experience { get; set; }
        public string Level { get; set; }
        
        public Developer()
        {
            base._id = Guid.NewGuid();
        }

        public Developer(string firstName, string lastName, EnumsForModels.TypeOfSex sex, string appointment, string date, 
                                                   int salary, string devLang, string experience, string level) 
                                                   : base(firstName, lastName, sex, appointment, date, salary)
        {
            base._id = Guid.NewGuid();
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

        /// <summary>
        /// Show all properties of worker in one string
        /// </summary>
        /// <returns>The string, contains properties of worker</returns>
        public override string ToString()
        {
            return String.Format("{0} {1}, {2}, {3}, work since {4}, salary: {5}, languages: {6}, experience: {7}, level: {8} ", FirstName, LastName, Sex.ToString().ToLower(), Appointment.ToLower(), Date, Salary,
                DevLang, Experience.ToLower(), Level.ToLower());
        }
    }
}
