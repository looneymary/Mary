using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    class Config
    {
        public string FilePath { get; set; }

        // Конструктор.
        public Config()
        {
            // Путь к файлу.
            FilePath = string.Format("{0} spisok.txt", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
