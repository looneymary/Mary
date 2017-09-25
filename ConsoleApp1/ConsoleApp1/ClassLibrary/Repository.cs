using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using ClassLibrary.Models;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ClassLibrary
{
    public class Repository
    {
        [XmlArray("Workers")]
        [XmlArrayItem("Developer", typeof(Developer))]
        [XmlArrayItem("OfficeWorker", typeof(OfficeWorker))]
        public List<Worker> People { get; set; }
        private WorkWithXml xml;

        public Repository()
        {
            People = new List<Worker>();
            xml = new WorkWithXml();
        }        

        /// <summary>
        /// Call a method of reading information from Xml-document 
        /// </summary>
        public void GetWorkersFromXml()
        {
            People.Clear();
            People = xml.ReadFromXml();
        }

        /// <summary>
        /// Get private list of workers
        /// </summary>
        /// <returns>The list of all workers</returns>
        public List<Worker> GetList()
        {
            return People;
        }

        /// <summary>
        /// Add object with propertiof of worker to list and write it to file
        /// </summary>
        /// <param name="obj">Object with properties of worker</param>
        public virtual void AddWorker(Worker obj)
        {
            this.People.Add(obj);
            var xmlObj = xml.ReadFromXmlInOneString();
            if(xmlObj != null)
            {
                xml.AddWorker(xmlObj, obj);
            }
        }

        /// <summary>
        /// Get one worker with some index number
        /// </summary>
        /// <param name="indexNumber">Parameter for search</param>
        /// <returns>Information of selected worker in string format</returns>
        public virtual string ShowOnePerson(int indexNumber)
        {             
            string result = "";
            if (this.People.Count >= indexNumber)
            {
                result = this.People.OrderBy(id => id._id).Where((x, i) => i == (indexNumber - 1)).First().ToString();
            }
            else
            {
                result = "The worker with that serial number is not found.";
            }
            return result;
        }
        
        /// <summary>
        /// Get all workers with selected appointment
        /// </summary>
        /// <param name="searchAppointment">Parameter for search</param>
        /// <returns>The list of workers' first name and last name with selected appointment</returns>
        public virtual string SearchByAppointment(string searchAppointment)
        {
            IEnumerable<Worker> selectPeople = this.People.Where(p => p.Appointment == searchAppointment);
            string result = "";
            if (selectPeople.Count() > 0 )
            {
                foreach (var res in selectPeople)
                {
                    result += string.Format("{0} {1}", res.LastName, res.FirstName) + "\n";
                }
            }
            else
            {
                result = "The search returns no results.";
            }
            return result;
        }

        /// <summary>
        /// Get all workers with selected name
        /// </summary>
        /// <param name="searchingName">Parameter for search</param>
        /// <returns>The list of workers with selectes name in string format</returns>
        public string SearchByName(string searchingName)
        {            
            IEnumerable<Worker> searchResult = this.People.Where(p => p.FirstName == searchingName);
            var count = this.People.Count(p => p.FirstName == searchingName);
            string result = "";
            if (count > 0)
            {
                foreach(var res in searchResult)
                {
                    result += res.ToString() + "\n";
                }
            }
            else
            {
                result = "The search returns no results.";
            }
            return result;
        }

        /// <summary>
        /// Count workers by appointment
        /// </summary>
        /// <param name="countAppointment">Parameter for count</param>
        /// <returns>List of appointments and employees' first name and last name holding these positions</returns>
        public string CountWorkers(string countAppointment)
        {            
            var count = this.People.Count(p => p.Appointment == countAppointment);
            string result = string.Format("{0} : {1}", countAppointment, count);
            return result;
        }

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
        
        /// <summary>
        /// Search and remove worker with selected index from list and from file
        /// </summary>
        /// <param name="number">Parameter for felete</param>
        /// <returns>String with the result of removing</returns>
        public string RemovePerson(int number)
        {
            string result = "";
            var index = People.FindAll(person => person._id == number);
            if(index.Count() != 0)
            {
                foreach( var person in People.OrderBy(person => person._id))
                {
                    if(person._id == number)
                    {
                        if(xml.RemoveFromXml(Config._xmlPath, number))
                        {
                            People.Remove(person);
                            result = "The worker was deleted.";
                        }                        
                    }
                }
            }
            else
            {
                result = "The worker with that serial number is not found.";
            }
            return result;
        }

        /// <summary>
        /// Get all workers who has "Developer" position
        /// </summary>
        /// <returns>The list of "Developers"</returns>
        public List<Worker> DeveloperWorkers()
        {
            var dev = from person in this.People
                      where IsDeveloper(person)
                      select person;
            List<Worker> developers = dev.ToList<Worker>();
            return developers;
        }

        /// <summary>
        /// Get all workers who has "OfficeWorker" position
        /// </summary>
        /// <returns>The list of "Office workers"</returns>
        public List<Worker> OfficeWorkers()
        {
            var officeWorkers = this.People.OfType<OfficeWorker>();
            List<Worker> office = officeWorkers.ToList<Worker>();
            return office;
        }
        
        /// <summary>
        /// Call a method that detects and adds a index number to worker
        /// </summary>
        /// <returns>Index number for worker</returns>
        public int AddIndexNumber()
        {
            int index = 1;
            
            GetWorkersFromXml();

            if (People.Count == 0)
            {
                return index;
            }
            else
            {
                foreach (var id in People.OrderBy(id => id._id))
                {
                    string[] elements = id.ToString().Split(' ');
                    if (index == int.Parse(elements[0]))
                    {
                        index++;
                    }
                    else
                    {
                        index = int.Parse(elements[0]) -1;
                        return index;
                    }
                }                
            }
            return index;
        }
    }
}
