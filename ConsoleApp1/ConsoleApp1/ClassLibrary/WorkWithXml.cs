using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using ClassLibrary.Models;
using System.ComponentModel;
using System.IO;

namespace ClassLibrary
{
    public class WorkWithXml
    {
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
            /// Add new worker in xml-document
            /// </summary>
            /// <param name="xml">Data of xml-documetn in one string</param>
            /// <param name="worker">Object that need to add in xml-document</param>
        public void AddWorker(string xml, Worker worker)
        {
            Repository repository = new Repository();
            Developer developer = new Developer();
            OfficeWorker office = new OfficeWorker();

            var stringReader = new StringReader(xml);
            XDocument xmlDocument = XDocument.Load(stringReader);
            var repositoryArray = xmlDocument.Element("Repository");
            var workers = repositoryArray.Element("Workers");

            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            if (repository.IsDeveloper(worker) == true)
            {
                developer = (Developer)worker;
                workers.AddFirst(new XElement("Developer",
                    new XElement("_id", developer._id),
                    new XElement("FirstName", developer.FirstName),
                    new XElement("LastName", developer.LastName),
                    new XElement("Sex", developer.Sex),
                    new XElement("Appointment", developer.Appointment),
                    new XElement("Date", developer.Date),
                    new XElement("Salary", developer.Salary),
                    new XElement("DeveloperLanguage", developer.DevLang),
                    new XElement("Experience", developer.Experience),
                    new XElement("Level", developer.Level)
                ));
            }
            else
            {
                office = (OfficeWorker)worker;
                workers.Add(new XElement("OfficeWorker",
                    new XElement("_id", office._id),
                    new XElement("FirstName", office.FirstName),
                    new XElement("LastName", office.LastName),
                    new XElement("Sex", office.Sex),
                    new XElement("Appointment", office.Appointment),
                    new XElement("Date", office.Date),
                    new XElement("Salary", office.Salary),
                    new XElement("YearsInService", office.YearsInService)
                ));
            }

            repositoryArray.Save(Config._xmlPath);
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
        /// Find anf remove worker by id from xml
        /// </summary>
        /// <param name="fileName">PAth to xml-document</param>
        /// <param name="id">Worker's id</param>
        /// <returns>Result of operation</returns>
        public bool RemoveFromXml(string fileName, int id)
        {
            XmlNode parent;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            if (!valid.Validate(Config._xmlPath, Config._xsdPath))
            {
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlNode xnode in xRoot)
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        foreach (XmlNode child in childnode.ChildNodes)
                        {
                            if (child.Name == "_id")
                            {
                                if (int.Parse(child.InnerText) == id)
                                {
                                    parent = child.ParentNode;
                                    xnode.RemoveChild(parent);
                                    xDoc.Save(fileName);
                                    return true;
                                }
                            }
                        }
                    }
                }                
            }
            else
            {
                Console.WriteLine("Xml-document isn't valid.");                
            }
            return false;
        }

        /// <summary>
        /// Deserialize data of xml-document
        /// </summary>
        /// <param name="fileName">Path to xmk-document</param>
        /// <returns></returns>
        public Repository DeserializeXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Repository));
            using (StreamReader sr = new StreamReader(fileName))
            {
                if (!valid.Validate(Config._xmlPath, Config._xsdPath))
                {
                    return (Repository)serializer.Deserialize(sr);
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
