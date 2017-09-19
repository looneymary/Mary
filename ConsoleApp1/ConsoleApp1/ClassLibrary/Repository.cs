using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using ClassLibrary.Models;

namespace ClassLibrary
{
    public class Repository
    {
        
        private List<Worker> people;
        private WorkWithFile file;
        private WorkWithXml xml; 

        public Repository()
        {
            people = new List<Worker>();
            file = new WorkWithFile();
            xml = new WorkWithXml();

            #region Xml
            GetWorkersFromXml();
            #endregion

            #region File
            //GetWorkersFromFile();
            #endregion
        }

        /// <summary>
        /// Call a method of reading information from file
        /// </summary>
        private void GetWorkersFromFile()
        {
            file.ReadFromFile(people);           
        }

        /// <summary>
        /// Call a method of reading information from Xml-document 
        /// </summary>
        public void GetWorkersFromXml()
        {            
            xml.ReadFromXml(people);
        }

        /// <summary>
        /// Get private list of workers
        /// </summary>
        /// <returns>The list of all workers</returns>
        public List<Worker> GetList()
        {
            return people;
        }

        /// <summary>
        /// Add object with propertiof of worker to list and write it to file
        /// </summary>
        /// <param name="obj">Object with properties of worker</param>
        public virtual void AddWorker(Worker obj)
        {
            this.people.Add(obj);

            #region work with xml
            WorkWithXml xml = new WorkWithXml();
            var xmlObj = xml.ReadFromXmlInOneString();
            xml.AddWorker(xmlObj, obj);
            #endregion

            #region work with file            
            //file.AddPersonToFile(obj);
            #endregion
        }

        /// <summary>
        /// Get one worker with some index number
        /// </summary>
        /// <param name="indexNumber">Parameter for search</param>
        /// <returns>Information of selected worker in string format</returns>
        public virtual string ShowOnePerson(int indexNumber)
        {             
            string result = "";
            if (this.people.Count >= indexNumber)
            {
                result = this.people.OrderBy(id => id._id).Where((x, i) => i == (indexNumber - 1)).First().ToString();
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
            IEnumerable<Worker> selectPeople = this.people.Where(p => p.Appointment == searchAppointment);
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
            IEnumerable<Worker> searchResult = this.people.Where(p => p.FirstName == searchingName);
            var count = this.people.Count(p => p.FirstName == searchingName);
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
            var count = this.people.Count(p => p.Appointment == countAppointment);
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
            var index = people.FindAll(person => person._id == number);
            if(index.Count() != 0)
            {
                foreach( var person in people.OrderBy(person => person._id))
                {
                    if(person._id == number)
                    {
                        people.Remove(person);
                    }
                }
                #region File
                //file.WriteToFile(people);
                #endregion

                #region Xml
                xml.RewriteXml(people);
                #endregion

                result = "The worker was deleted.";
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
            var dev = from person in this.people
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
            var officeWorkers = this.people.OfType<OfficeWorker>();
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

            #region File
            //GetWorkersFromFile();
            #endregion

            #region Xml
            GetWorkersFromXml();
            #endregion

            if (people.Count == 0)
            {
                return index;
            }
            else
            {
                foreach (var id in people.OrderBy(id => id._id))
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
