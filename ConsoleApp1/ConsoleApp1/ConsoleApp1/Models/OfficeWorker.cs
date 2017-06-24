using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    class OfficeWorker : Repository
    {
        public delegate int ValidValuesDelegate(string a, string b, string c, string d, string f, int g);
        public event ValidValuesDelegate СheckingValid;

        public OfficeWorker() : base()
        {            
        }

        public override void AddInfo()
        {
            base.AddInfo();
            СheckingValid(FirstName, LastName, Sex.ToString(), Appointment, Date, Salary);            
        }
    }
}
