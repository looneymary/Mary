using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    abstract class Worker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Appointment { get; set; }
        public string Date { get; set; }
        public int Salary { get; set; }
        
        public string FullInfo { get; set; }
        
        // Конструктор.
        public Worker()
        {
            FullInfo = "";
        }

        // Переопределения метода ToString.
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", LastName, FirstName, Sex, Appointment, Date, Salary);
        }
    }
}
