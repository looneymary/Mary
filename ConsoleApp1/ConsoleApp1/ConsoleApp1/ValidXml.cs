using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using ClassLibrary;
using ClassLibrary.Models;
using System.ComponentModel;
using System.IO;

namespace ConsoleApp1
{
    class ValidXml
    {
        

        public void Validate(string xmlPath, string xsdPath)
        {
            XDocument xmlFile = XDocument.Load(xmlPath);
            XmlSchemaSet xsdSchema = new XmlSchemaSet();
            xsdSchema.Add(null, xsdPath);
            bool errors = false;
            xmlFile.Validate(xsdSchema, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                errors = true;
            });
            Console.WriteLine("doc1 {0}", errors ? "did not validate" : "validated");
        }

        public void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine(e.Message); 
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine(e.Message);
                    break;
            }
        }
    }
}
