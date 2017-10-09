﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;

namespace BusinessLayer
{
    public class BusinessLayerClass
    {
        private XmlRepository repository;

        public BusinessLayerClass()
        {
            repository = new XmlRepository();
        }

        /// <summary>
        /// Get one worker with some index number
        /// </summary>
        /// <param name="indexNumber">Parameter for search</param>
        /// <returns>Information of selected worker in string format</returns>
        public virtual void ShowOnePerson(int indexNumber)
        {
            foreach (var person in repository.Get("Workers/*["+ indexNumber + "]"))
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
            IEnumerable<Worker> selectPeople = repository.Get("Workers/*[Appointment = '" + searchAppointment + "']");

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
            IEnumerable<Worker> searchResult = repository.Get("Workers/*[Appointment = '" + searchingName + "']");

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
            IEnumerable<Worker> workers = repository.Get("Workers/*[Appointment = '" + countAppointment + "']");

            string result = string.Format("{0} : {1}", countAppointment, workers.Count());
            return result;
        }
    }
}