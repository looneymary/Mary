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
        public string ReadFromXmlInOneString()
        {
            using (StreamReader sr = new StreamReader(Config._xmlPath))
            {
                String line = sr.ReadToEnd();
                return line;
            }
        }

        public void ReadFromXml(List<Worker> people)
        {
            people.Clear();

            var serializer = new XmlSerializer(typeof(List<Worker>));
            using (StreamReader sr = new StreamReader(Config._xmlPath))
            {
                var deserializeWorkers = (List<Worker>)serializer.Deserialize(sr);
                foreach(var line in deserializeWorkers)
                {
                    string[] names = line.ToString().Split(' ');

                    int id = int.Parse(names[0]);
                    string firstName = names[1];
                    string lastName = names[2];
                    EnumsForModels.TypeOfSex sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), names[3]);
                    string appointment = names[4];
                    string date = names[5];
                    int salary = int.Parse(names[6]);

                    if (names.Length == 10)
                    {
                        string devLang = names[7];
                        string experience = names[8];
                        string level = names[9];
                        Developer developer = new Developer(id, firstName, lastName, sex, appointment, date, salary, devLang, experience, level);
                        people.Add(developer);
                    }
                    if (names.Length == 8)
                    {
                        int yearsInService = int.Parse(names[7]);
                        OfficeWorker office = new OfficeWorker(id, firstName, lastName, sex, appointment, date, salary, yearsInService);
                        people.Add(office);
                    }
                }                
            }                
        }

        public void AddWorker(string xml, Worker worker)
        {
            Repository repository = new Repository();
            Developer developer = new Developer();
            OfficeWorker office = new OfficeWorker();

            var stringReader = new StringReader(xml);
            XDocument xmlDocument = XDocument.Load(stringReader);
            var workers = xmlDocument.Element("ArrayOfWorker");
            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            if (repository.IsDeveloper(worker) == true)
            {                
                developer = (Developer)worker;
                workers.Add(new XElement("Worker",
                                new XAttribute(ns + "type", "Developer"),
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
                workers.Add(new XElement("Worker",
                                new XAttribute(ns + "type", "OfficeWorker"),
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

            workers.Save(Config._xmlPath);
        }

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

        public void RewriteXml(List<Worker> workers)
        {            
            string stringForRewrite = SerializeObject(workers);
            var stringReader = new StringReader(stringForRewrite);
            XDocument xmlDocument = XDocument.Load(stringReader);
            xmlDocument.Save(Config._xmlPath);
        }
    }
}
