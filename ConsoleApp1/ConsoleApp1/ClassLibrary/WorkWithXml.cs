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
        public enum ElementsOfXml { FirstName = 0, LastName = 1, Sex = 2, Appointment = 3, Date = 4, Salary = 5,
                                    DeveloperLanguage = 6, Experience = 7, Level = 8, YearsInService = 9 } 

        ValidXml valid = new ValidXml();

        /// <summary>
        /// Read from xml-document and put data in the string
        /// </summary>
        /// <returns>The string with all strings from the xml-document</returns>
        public string ReadFromXmlInOneString(string xmlPath, string xsdPath)
        {
            using (StreamReader streamReader = new StreamReader(xmlPath))
            {
                if (!valid.Validate(xmlPath, xsdPath))
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
        public List<Worker> ReadFromXml(string xmlPath, string xsdPath)
        {
            var deserializeXml = DeserializeXml(xmlPath, xsdPath);
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
        public string SerializeObject(XmlRepository workers)
        {
            XmlSerializer serializer = new XmlSerializer(workers.GetType());
            using (StringWriter stringwriter = new System.IO.StringWriter())
            {
                serializer.Serialize(stringwriter, workers);
                return stringwriter.ToString();
            }
        }

        /// <summary>
        /// Deserialize data of xml-document
        /// </summary>
        /// <param name="fileName">Path to xmk-document</param>
        /// <returns></returns>
        public XmlRepository DeserializeXml(string xmlPath, string xsdPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlRepository));
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                if (!valid.Validate(xmlPath, xsdPath))
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
    }
}
