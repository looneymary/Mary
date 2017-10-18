using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using DataAccess.Models;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataAccess
{
    public class XmlRepository : IRepository
    {
        [XmlArray("Workers")]
        [XmlArrayItem("Developer", typeof(Developer))]
        [XmlArrayItem("OfficeWorker", typeof(OfficeWorker))]
        public List<Worker> People { get; set; }
        string xmlPath = Config._xmlPath;
        string xsdPath = Config._xsdPath;

        private WorkWithXml _xml;
        ValidXml valid = new ValidXml();

        public XmlRepository()
        {
            People = new List<Worker>();
            this._xml = new WorkWithXml();
        }

        /// <summary>
        /// Get list of workers according to certain filter
        /// </summary>
        /// <param name="filter">Filter for search</param>
        /// <returns>List of workers what found</returns>
        public IEnumerable<Worker> Get(string filter)
        {
            List<Worker> Workers = new List<Worker>();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            if (!valid.Validate(xmlPath, xsdPath))
            {
                XmlElement _root = doc.DocumentElement;

                XmlNodeList people = _root.SelectNodes(filter);

                foreach (XmlNode person in people)
                {
                    XDocument xDoc = XDocument.Parse(person.OuterXml);

                    var developers = xDoc.Elements("Developer")
                               .Select(element => new Developer()
                               {
                                   _id = Guid.Parse(element.Element("_id").Value),
                                   FirstName = element.Element("FirstName").Value,
                                   LastName = element.Element("LastName").Value,
                                   Sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), element.Element("Sex").Value),
                                   Appointment = element.Element("Appointment").Value,
                                   Date = element.Element("Date").Value,
                                   Salary = int.Parse(element.Element("Salary").Value),
                                   DevLang = element.Element("DeveloperLanguage").Value,
                                   Experience = element.Element("Experience").Value,
                                   Level = element.Element("Level").Value
                               });

                    if (developers != null)
                    {
                        foreach (var developer in developers)
                        {
                            Workers.Add(developer);
                        }
                    }

                    var officeWorkers = xDoc.Elements("OfficeWorker")
                               .Select(element => new OfficeWorker()
                               {
                                   _id = Guid.Parse(element.Element("_id").Value),
                                   FirstName = element.Element("FirstName").Value,
                                   LastName = element.Element("LastName").Value,
                                   Sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), element.Element("Sex").Value),
                                   Appointment = element.Element("Appointment").Value,
                                   Date = element.Element("Date").Value,
                                   Salary = int.Parse(element.Element("Salary").Value),
                                   YearsInService = int.Parse(element.Element("YearsInService").Value)
                               });

                    if (officeWorkers != null)
                    {
                        foreach (var office in officeWorkers)
                        {
                            Workers.Add(office);
                        }
                    }

                }
            }
            return Workers;
        }

        /// <summary>
        /// Add new worker in xml-document
        /// </summary>
        /// <param name="worker">Object that need to add in xml-document</param>
        public void Create(Worker worker)
        {
            Developer developer = new Developer();
            OfficeWorker office = new OfficeWorker();

            var streamReader = new StreamReader(xmlPath);
            XDocument xmlDocument = XDocument.Load(streamReader);
            if(!valid.Validate(xmlPath, xsdPath))
            {
                var repositoryArray = xmlDocument.Element("XmlRepository");
                var workers = repositoryArray.Element("Workers");

                XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
                if (worker is Developer == true)
                {
                    developer = (Developer)worker;
                    workers.AddFirst(new XElement("Developer",
                        new XElement("_id", "{" + developer._id + "}"),
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
                        new XElement("_id", "{" + office._id + "}"),
                        new XElement("FirstName", office.FirstName),
                        new XElement("LastName", office.LastName),
                        new XElement("Sex", office.Sex),
                        new XElement("Appointment", office.Appointment),
                        new XElement("Date", office.Date),
                        new XElement("Salary", office.Salary),
                        new XElement("YearsInService", office.YearsInService)
                    ));
                }

                streamReader.Close();
                repositoryArray.Save(xmlPath);
            }
        }

        /// <summary>
        /// Update element in xml-document
        /// </summary>
        /// <param name="worker">object "Worker" for update</param>        
        public void Update(Worker worker)
        {
            Developer developer = new Developer();
            OfficeWorker office = new OfficeWorker();

            StreamReader streamReader = new StreamReader(xmlPath);
            XDocument xmlDocument = XDocument.Load(streamReader);
            if(!valid.Validate(xmlPath, xsdPath))
            {
                var repositoryArray = xmlDocument.Element("XmlRepository");
                var workers = repositoryArray.Element("Workers").Elements();

                foreach (var xNode in workers)
                {
                    if (Guid.Parse(xNode.Element("_id").Value) == worker._id)
                    {
                        if (worker is Developer == true)
                        {
                            developer = (Developer)worker;
                            xNode.SetElementValue("FirstName", developer.FirstName);
                            xNode.SetElementValue("LastName", developer.LastName);
                            xNode.SetElementValue("Sex", developer.Sex.ToString());
                            xNode.SetElementValue("Appointment", developer.Appointment);
                            xNode.SetElementValue("Date", developer.Date);
                            xNode.SetElementValue("Salary", developer.Salary.ToString());
                            xNode.SetElementValue("DeveloperLanguage", developer.DevLang);
                            xNode.SetElementValue("Experience", developer.Experience.ToString());
                            xNode.SetElementValue("Level", developer.Level);
                        }
                        else
                        {
                            office = (OfficeWorker)worker;
                            xNode.SetElementValue("FirstName", office.FirstName);
                            xNode.SetElementValue("LastName", office.LastName);
                            xNode.SetElementValue("Sex", office.Sex.ToString());
                            xNode.SetElementValue("Appointment", office.Appointment);
                            xNode.SetElementValue("Date", office.Date);
                            xNode.SetElementValue("Salary", office.Salary.ToString());
                            xNode.SetElementValue("YearsInService", office.YearsInService.ToString());
                        }
                        streamReader.Close();
                        repositoryArray.Save(xmlPath);
                        break;
                    }
                }
            }
        }

        /// <summary>
            /// Find anf remove worker by id from xml
            /// </summary>
            /// <param name="id">Worker's id</param>
        public void Delete(Guid id)
        {            
            XElement xDoc = XElement.Load(xmlPath);
            if(!valid.Validate(xmlPath, xsdPath))
            {
                IEnumerable<XElement> elements = xDoc.Element("Workers").Elements().Elements("_id");
                foreach (XElement xNode in elements)
                {
                    if (Guid.Parse(xNode.Value) == id)
                    {
                        xNode.Parent.Remove();
                        xDoc.Save(xmlPath);
                        Console.WriteLine("Removing was sucсessful");
                    }
                }
            }            
        }
    }
}
