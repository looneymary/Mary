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

        protected string FilePath { get; set; }
        public string FullInfo { get; set; }
        public List<object> people;

        //Конструктор
        public Worker()
        {
            //Путь к файлу
            FilePath = string.Format("{0} spisok.txt", AppDomain.CurrentDomain.BaseDirectory);

            FullInfo = "";
        }
    }
}
