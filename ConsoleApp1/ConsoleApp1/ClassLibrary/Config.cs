using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ClassLibrary
{
    public class Config
    {
        public static readonly string _xmlPath;
        public static readonly string _xsdPath;

        // Constructor.
        static Config()
        {
            // The path to xml & xsd.
            _xmlPath = string.Format("{0}XMLFile1.xml", AppDomain.CurrentDomain.BaseDirectory);
            _xsdPath = string.Format("{0}XMLSchema1.xsd", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
