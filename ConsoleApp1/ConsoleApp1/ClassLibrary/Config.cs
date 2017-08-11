using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ClassLibrary
{
    class Config
    {
        public static readonly string _filePath;

        // Constructor.
        static Config()
        {
            // The path to file.
            _filePath = string.Format("{0}spisok.txt", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
