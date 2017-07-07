﻿using System;
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
            string searchAppointment = Console.ReadLine();
            IEnumerable<Worker> selectPeople = people.Where(p => p.Appointment == searchAppointment);
            if (selectPeople.Count() > 0 )
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
            string countAppointment = Console.ReadLine();
            var count = people.Count(p => p.Appointment == countAppointment);
            Console.WriteLine("{0} : {1}", countAppointment, count);
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
            WorkWithFile file = new WorkWithFile();

            Console.WriteLine("Введите номер сотрудника: ");
            int number = int.Parse(Console.ReadLine());
            number -= 1;
            
            people.RemoveAt(number);
            file.WriteToFile(people);

            Console.WriteLine("Сотрудник удалён");
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