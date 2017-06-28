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
        public List<Repository> onePerson = new List<Repository>();
        public List<Repository> developers = new List<Repository>();
        public List<Repository> officeWorkers = new List<Repository>();

        // Добавить.
        public virtual void AddWorker(Worker obj)
        {            
            people.Add(obj);
        }
        
        // Выбор одного сотрудника.
        public virtual void ShowOnePerson(List<Worker> people)
        {
            Console.WriteLine("Введите порядковый номер:");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine(people.Where((x, i) => i == (num - 1)).First().ToString());
        }
        
        // Search by appointment.
        public virtual void SearchByAppointment(List<Worker> people)
        {
            Console.WriteLine("Введите должность: ");
            string SearchAppointment = Console.ReadLine();
            IEnumerable<Worker> selectPeople = people.Where(p => p.Appointment == SearchAppointment);
            var count = people.Count(p => p.Appointment == SearchAppointment);
            if (count > 0)
            {
                foreach (var res in selectPeople)
                {
                    Console.WriteLine(string.Format("{0} {1}", res.LastName, res.FirstName));
                }
            }
            else
            {
                Console.WriteLine("Поиск не дал результатов.");
            }
        }

        //Search person by name.
        public void SearchByName(List<Worker> people)
        {
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            IEnumerable<Worker> searchResult = people.Where(p => p.FirstName == name);
            var count = people.Count(p => p.FirstName == name);
            if (count > 0)
            {
                foreach(var res in searchResult)
                {
                     Console.WriteLine(res);
                }
            }
            else
            {
                Console.WriteLine("Tse search returns no results.");
            }
        }

        // Count workers.
        public void CountWorkers(List<Worker> people)
        {
            Console.WriteLine("Введите должность: ");
            string CountAppointment = Console.ReadLine();
            var count = people.Count(p => p.Appointment == CountAppointment);
            Console.WriteLine("{0} : {1}", CountAppointment, count);
        }
        
        // Is he developer.
        public bool IsDeveloper(Object obj)
        {
            bool val = obj is Developer;
            return val;
        }
        
        // Delete worker.
        public void RemovePerson(List<Worker> people)
        {
            Console.WriteLine("Введите номер сотрудника: ");
            int number = int.Parse(Console.ReadLine());
            number -= 1;
            
            people.RemoveAt(number);

            Console.WriteLine("Сотрудник удалён");
        }

        public List<Worker> DeveloperWorkers(List<Worker> people)
        {
            var developers = people.OfType<Developer>();
            List<Worker> dev = developers.ToList<Worker>();
            return dev;
        }

        public List<Worker> OfficeWorkers(List<Worker> people)
        {
            var officeWorkers = people.OfType<OfficeWorker>();
            List<Worker> office = officeWorkers.ToList<Worker>();
            return office;
        }

    }
}
