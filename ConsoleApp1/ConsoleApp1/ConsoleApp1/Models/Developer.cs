using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1.Models
{
    class Developer : Repository
    {
        Repository repository = new Repository();

        public string DevLang { get; set; }
        public string Experience { get; set; }
        public string Level { get; set; }

        public Developer() : base()
        {
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
        }
    }
}
