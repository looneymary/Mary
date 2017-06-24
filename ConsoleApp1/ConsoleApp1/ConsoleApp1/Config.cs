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
        public static readonly string _filePath;

        // Конструктор.
        static Config()
        {
            // Путь к файлу.
            _filePath = string.Format("{0} spisok.txt", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
