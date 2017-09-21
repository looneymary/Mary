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

namespace ClassLibrary
{
    public class ValidXml
    {
        /// <summary>
        /// Valid xml-document
        /// </summary>
        /// <param name="xmlPath">Path to the xml-document</param>
        /// <param name="xsdPath">Path to the xsd-schema</param>
        public bool Validate(string xmlPath, string xsdPath)
        {
            bool errors = false;

            XDocument xmlFile = XDocument.Load(xmlPath);
            XmlSchemaSet xsdSchema = new XmlSchemaSet();
            xsdSchema.Add(null, xsdPath);

            xmlFile.Validate(xsdSchema, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                errors = true;
            });
            return errors;
        }
    }
}
