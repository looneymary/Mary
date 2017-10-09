using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using DataAccess.Models;
using System.ComponentModel;
using System.IO;

namespace DataAccess
{
    public class WorkWithXml
    {
        public enum ElementsOfXml { FirstName = 1, LastName = 2, Sex = 3, Appointment = 4, Date = 5, Salary = 6,
                                    DeveloperLanguage = 7, Experience = 8, Level = 9, YearsInService = 10 } 

        ValidXml valid = new ValidXml();

        /// <summary>
        /// Read from xml-document and put data in the string
        /// </summary>
        /// <returns>The string with all strings from the xml-document</returns>
        public string ReadFromXmlInOneString()
        {
            using (StreamReader streamReader = new StreamReader(Config._xmlPath))
            {
                if (!valid.Validate(Config._xmlPath, Config._xsdPath))
                {
                    String line = streamReader.ReadToEnd();
                    return line;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Read from xml-documet and add info in List<Worker>
        /// </summary>
        /// <param name="people">List of workers</param>
        public List<Worker> ReadFromXml()
        {
            var deserializeXml = DeserializeXml(Config._xmlPath);
            if (deserializeXml != null)
            {
                return deserializeXml.People;
            }
            else
            {
                return null;
            }
        }        

        /// <summary>
        /// Serialize list of workers
        /// </summary>
        /// <param name="workers">List of workers</param>
        /// <returns></returns>
        public string SerializeObject(List<Worker> workers)
        {
            using (StringWriter stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(workers.GetType());
                serializer.Serialize(stringwriter, workers);
                var obj = stringwriter.ToString();
                return obj;
            }
        }                      

        /// <summary>
        /// Deserialize data of xml-document
        /// </summary>
        /// <param name="fileName">Path to xmk-document</param>
        /// <returns></returns>
        public XmlRepository DeserializeXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlRepository));
            using (StreamReader sr = new StreamReader(fileName))
            {
                if (!valid.Validate(Config._xmlPath, Config._xsdPath))
                {
                    return (XmlRepository)serializer.Deserialize(sr);
                }
                else
                {
                    Console.WriteLine("Xml-document isn't valid");
                    return null;
                }
            }
        }        

        /// <summary>
        /// Update xnl-document
        /// </summary>
        /// <param name="fileName">The path of xml-document </param>
        /// <returns>Result of updating</returns>
        public bool UpdateXml(string fileName, Guid id, int numOfElement, string value)
        {
            ElementsOfXml elementName = (ElementsOfXml)numOfElement;
            
            XElement xDoc = XElement.Load(fileName);

            if (!valid.Validate(Config._xmlPath, Config._xsdPath))
            {
                IEnumerable<XElement> elements = xDoc.Element("Workers").Elements();

                foreach (XElement xNode in elements)
                {
                    if (Guid.Parse(xNode.Element("_id").Value) == id)
                    {
                        xNode.Element(elementName.ToString()).Value = value;
                        xDoc.Save(fileName);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
