using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using System.Xml;
using System.Xml.Linq;

namespace BusinessLayer
{
    public class BusinessLayerMethods
    {
        private IRepository _repository;

        public BusinessLayerMethods(IRepository _repository)
        {
            this._repository = new XmlRepository();
        }

        public BusinessLayerMethods(IRepository _repository, string xmlFile)
        {
            this._repository = new XmlRepository(xmlFile);
        }

        /// <summary>
        /// Get one worker with some index number
        /// </summary>
        /// <param name="indexNumber">Parameter for search</param>
        /// <returns>Information of selected worker in string format</returns>
        public virtual void ShowOnePerson(int indexNumber)
        {
            foreach (var person in this._repository.Get("Workers/*["+ indexNumber + "]"))
            {
                Console.WriteLine(person);
            }
        }

        /// <summary>
        /// Get all workers with selected appointment
        /// </summary>
        /// <param name="searchAppointment">Parameter for search</param>
        public virtual void SearchByAppointment(string searchAppointment)
        {
            IEnumerable<Worker> selectPeople = this._repository.Get("Workers/*[Appointment = '" + searchAppointment + "']");

            if (selectPeople.Count() > 0)
            {
                foreach (var person in selectPeople)
                {
                    Console.WriteLine(person);
                }
            }
            else
            {
                Console.WriteLine("The search returns no results.");
            }
        }

        /// <summary>
        /// Get all workers with selected name
        /// </summary>
        /// <param name="searchingName">Parameter for search</param>
        public void SearchByName(string searchingName)
        {
            IEnumerable<Worker> searchResult = this._repository.Get("Workers/*[Appointment = '" + searchingName + "']");

            if (searchResult.Count() > 0)
            {
                foreach (var res in searchResult)
                {
                    Console.WriteLine(res);
                }
            }
            else
            {
                Console.WriteLine("The search returns no results.");
            }
        }

        /// <summary>
        /// Count workers by appointment
        /// </summary>
        /// <param name="countAppointment">Parameter for count</param>
        /// <returns>List of appointments and employees' first name and last name holding these positions</returns>
        public string CountWorkers(string countAppointment)
        {
            IEnumerable<Worker> workers = this._repository.Get("Workers/*[Appointment = '" + countAppointment + "']");

            string result = string.Format("{0} : {1}", countAppointment, workers.Count());
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
        /// Call method Get of IRepository
        /// </summary>
        /// <param name="filter">Filter for search</param>
        /// <returns>List of workers what found</returns>
        public IEnumerable<Worker> Get(string filter)
        {
            return this._repository.Get(filter);
        }

        /// <summary>
        /// Call method Create of IRepository
        /// </summary>
        /// <param name="worker">Object that need to add in xml-document</param>
        public void Create(Worker worker)
        {
            this._repository.Create(worker);
        }

        /// <summary>
        /// Call method Update of IRepository
        /// </summary>
        /// <param name="worker">object "Worker" for update</param>
        public void Update(Worker worker)
        {
            this._repository.Update(worker);
        }

        /// <summary>
        /// Call method Delete of IRepository
        /// </summary>
        /// <param name="id">Worker's id</param>
        public void Delete(Guid id)
        {
            this._repository.Delete(id);
        }        
    }
}
