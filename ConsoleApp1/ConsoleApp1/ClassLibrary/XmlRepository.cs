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

        private WorkWithXml xml;
        ValidXml valid = new ValidXml();

        public XmlRepository()
        {
            People = new List<Worker>();
            xml = new WorkWithXml();
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
            doc.Load(Config._xmlPath);

            if (!valid.Validate(Config._xmlPath, Config._xsdPath))
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
            XmlRepository repository = new XmlRepository();
            Developer developer = new Developer();
            OfficeWorker office = new OfficeWorker();

            var streamReader = new StreamReader(Config._xmlPath);
            XDocument xmlDocument = XDocument.Load(streamReader);
            var repositoryArray = xmlDocument.Element("XmlRepository");
            var workers = repositoryArray.Element("Workers");

            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            if (repository.IsDeveloper(worker) == true)
            {
                developer = (Developer)worker;
                workers.AddFirst(new XElement("Developer",
                    new XElement("_id", "{"+ developer._id + "}"),
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
            repositoryArray.Save(Config._xmlPath);
        }

        /// <summary>
        /// Update element in xml-document
        /// </summary>
        /// <param name="worker">object "Worker" for update</param>        
        public void Update(Worker worker)
        {

        }    

        /// <summary>
        /// Find anf remove worker by id from xml
        /// </summary>
        /// <param name="id">Worker's id</param>
        public void Delete(Guid id)
        {            
            XElement xDoc = XElement.Load(Config._xmlPath);
            IEnumerable<XElement> elements = xDoc.Element("Workers").Elements().Elements("_id");
            foreach (XElement xNode in elements)
            {
                if (Guid.Parse(xNode.Value) == id)
                {
                    xNode.Parent.Remove();
                    xDoc.Save(Config._xmlPath);
                    Console.WriteLine("Removing was sucсessful");
                }
            }
        }        

        //ВЫНЕСТИ В БИЗНЕС
        /// <summary>
        /// Check whether the object is a type "Developer"
        /// </summary>
        /// <param name="obj">Object for check</param>
        /// <returns>Boolean value</returns>
        public bool IsDeveloper(Object obj)
        {
            bool val = obj is Developer;
            return val;
        }
    }
}
