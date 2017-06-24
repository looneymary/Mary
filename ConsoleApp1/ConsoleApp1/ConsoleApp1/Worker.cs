using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    public abstract class Worker
    {
        public Guid _id;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EnumHelper.TypeOfSex Sex { get; set; }
        public string Appointment { get; set; }
        public string Date { get; set; }
        public int Salary { get; set; }
        
        public string FullInfo { get; set; }
                
        // Конструктор.
        public Worker()
        {          
        }

        // Переопределения метода ToString.
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", LastName, FirstName, Sex, Appointment, Date, Salary);
        }
    }
}
