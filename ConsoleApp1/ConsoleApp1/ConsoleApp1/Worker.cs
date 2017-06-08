using System;
using System.Collections;
using System.IO;

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
        public ArrayList fullInfoArr;

        //Конструктор
        public Worker()
        {
            //Путь к файлу
            FilePath = string.Format("{0} spisok.txt", AppDomain.CurrentDomain.BaseDirectory);

            //Коллекция строк со всей инфой для записи в файл
            fullInfoArr = new ArrayList();

            FullInfo = "";
        }
    }
}
