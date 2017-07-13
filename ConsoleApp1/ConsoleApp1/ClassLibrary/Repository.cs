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
        public List<Worker> people = new List<Worker>();

        // Добавить.
        public virtual void AddWorker(Worker obj)
        {
            WorkWithFile file = new WorkWithFile();
            people.Add(obj);
            file.AddPersonToFile(obj);
        }

        // Выбор одного сотрудника.
        public virtual string ShowOnePerson(List<Worker> people, int indexNumber)
        {             
            string result = "";
            if (people.Count > indexNumber)
            {
                result = people.Where((x, i) => i == (indexNumber - 1)).First().ToString();
            }
            else
            {
                result = "Сотрудник с таким порядковым номером не найден.";
            }
            return result;
        }
        
        // Search by appointment.
        public virtual string SearchByAppointment(List<Worker> people, string searchAppointment)
        {
            IEnumerable<Worker> selectPeople = people.Where(p => p.Appointment == searchAppointment);
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

        //Search person by name.
        public string SearchByName(List<Worker> people, string searchingName)
        {            
            IEnumerable<Worker> searchResult = people.Where(p => p.FirstName == searchingName);
            var count = people.Count(p => p.FirstName == searchingName);
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

        // Count workers.
        public string CountWorkers(List<Worker> people, string countAppointment)
        {            
            var count = people.Count(p => p.Appointment == countAppointment);
            string result = string.Format("{0} : {1}", countAppointment, count);
            return result;
        }
        
        // Is he developer.
        public bool IsDeveloper(Object obj)
        {
            bool val = obj is Developer;
            return val;
        }
        
        // Delete worker.
        public string RemovePerson(List<Worker> people, int number)
        {
            WorkWithFile file = new WorkWithFile();
            number -= 1;
            string result = "";
            if(people.Count > number)
            {
                people.RemoveAt(number);
                file.WriteToFile(people);

                result = "Сотрудник удалён";
            }
            else
            {
                result = "Сотрудник с таким порядковым номером не найден.";
            }
            return result;
        }

        public List<Worker> DeveloperWorkers(List<Worker> people)
        {
            List<Worker> dev = new List<Worker>();
            foreach (var person in people)
            {
                if (IsDeveloper(person))
                {
                    
                    dev.Add(person);
                }
            }
            return dev;
        }

        public List<Worker> OfficeWorkers(List<Worker> people)
        {
            var officeWorkers = people.OfType<OfficeWorker>();
            List<Worker> office = officeWorkers.ToList<Worker>();
            return office;
        }

        public void GetWorkersFromFile()
        {
            WorkWithFile file = new WorkWithFile();
            file.ReadFromFile(people);
        }
    }
}
