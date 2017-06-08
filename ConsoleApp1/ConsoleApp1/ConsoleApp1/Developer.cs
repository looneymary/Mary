using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    class Developer : Methods
    {
        public string DevLang { get; set; }
        public string Experience { get; set; }
        public string Level { get; set; }
        public string FullDevInfo { get; set; }
        public ArrayList fullDevArr { get; set; }

        public Developer() : base()
        {
            fullDevArr = new ArrayList();
        }

        public override void AddInfo()
        {
            base.AddInfo();
            Console.WriteLine("\nДанные разработчика: \n");

            Console.WriteLine("Язык разработки:");
            DevLang = Console.ReadLine();

            Console.WriteLine("Стаж:");
            Experience = Console.ReadLine();

            Console.WriteLine("Уровень знаний:");
            Level = Console.ReadLine();

            FullDevInfo += string.Format(" {0, 20}   {1, 10}   {2, 3}   ", DevLang, Experience, Level);
            fullDevArr.Add(FullDevInfo);
        }
    }
}
