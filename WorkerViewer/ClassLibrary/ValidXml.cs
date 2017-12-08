using System;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DataAccess
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
                errors = true;
                throw e.Exception;
            });
            return errors;
        }
    }
}
