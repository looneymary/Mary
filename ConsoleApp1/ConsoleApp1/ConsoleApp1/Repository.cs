using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    public class Repository : Worker
    {
        public List<Repository> people = new List<Repository>();
        public List<Repository> onePerson = new List<Repository>();
        public List<Repository> developers = new List<Repository>();
        public List<Repository> officeWorkers = new List<Repository>();
        CheckValidExceptions ex = new CheckValidExceptions();        

        public Repository() : base()
        {
        }              

        // Добавить.
        public virtual void AddInfo()
        {
            CheckValidExceptions ex = new CheckValidExceptions();
            Console.WriteLine("Имя:");
            FirstName = Console.ReadLine();
            
            Console.WriteLine("Фамилия:");
            LastName = Console.ReadLine();

            Console.WriteLine("Пол: м - 1/ ж - 2.");
            try
            {
                string Val = Console.ReadLine();
                Sex = (EnumHelper.TypeOfSex)int.Parse(Val);
            }
            catch (System.FormatException)
            {
            }

            Console.WriteLine("Должность:");
            Appointment = Console.ReadLine();

            Console.WriteLine("\nРабочая информация: \n");

            Console.WriteLine("Дата вступления в должность:");
            Date = Console.ReadLine();

            Console.WriteLine("Оклад:");
            try
            {                
                string Val = Console.ReadLine();
                Salary = int.Parse(Val);

            }
            catch (System.FormatException)
            {                
            } 
        }

        // Search by appointment.
        public virtual void SearchByAppointment(List<Repository> people)
        {
            Console.WriteLine("Введите должность: ");
            string SearchAppointment = Console.ReadLine();
            IEnumerable<Repository> selectPeople = people.Where(p => p.Appointment == SearchAppointment);
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
        public void SearchByName(List<Repository> people)
        {
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            IEnumerable<Repository> searchResult = people.Where(p => p.FirstName == name);
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

        // Выбор одного сотрудника.
        public virtual void ShowOnePerson(List<Repository> people)
        {
            onePerson.Clear();
            Console.WriteLine("Введите порядковый номер:");
            int num = int.Parse(Console.ReadLine());
            onePerson.Add(people[num - 1]);
        }

        // Подсчёт.
        public void CountWorkers(List<Repository> people)
        {
            Console.WriteLine("Введите должность: ");
            string CountAppointment = Console.ReadLine();
            var count = people.Count(p => p.Appointment == CountAppointment);
            Console.WriteLine("{0} : {1}", CountAppointment, count);
        }
        
        // Является ли разработчиком.
        public bool IsDeveloper(Object obj)
        {
            bool val = obj is Developer;
            return val;
        }
        
        // Удаление сотрудника.
        public void RemovePerson()
        {
            WorkWithFile file = new WorkWithFile();

            Console.WriteLine("Введите номер сотрудника: ");
            int number = int.Parse(Console.ReadLine());
            number -= 1;

            RemoveFromAnyList(developers, people[number]);
            RemoveFromAnyList(officeWorkers, people[number]);
            people.RemoveAt(number);

            Console.WriteLine("Сотрудник удалён");
            file.WriteToFile(developers, officeWorkers);
        }

        //Remove from developers/officeworkers
        public void RemoveFromAnyList(List<Repository> obj1, Object obj2)
        {
            int i = -1;
            foreach (var person in obj1)
            {
                if (person.Equals(obj2))
                {
                    i = obj1.IndexOf(person);
                }
            }
            if (i >= 0)
            {
                obj1.RemoveAt(i);
            }
        }

        // For checking exceptions in AddInfo.
        public void AddPerson(Repository obj, CheckValidExceptions ex)
        {
            WorkWithFile file = new WorkWithFile();

            int countExceptions = ex.ValidResult;
            if (countExceptions == 0)
            {
                // Добавить пользователя в коллекцию.
                people.Add(obj);
                SortPeopleInDifferentTables(obj);
                file.AddPersonToFile(obj);
            }
        }

        //Sort into different tables
        public void SortPeopleInDifferentTables(Repository obj)
        {
            bool res = IsDeveloper(obj);
            if (res == true)
            {
                developers.Add(obj);
            }
            else
            {
                officeWorkers.Add(obj);
            }           
        }

        //Show people into different tables
        public void ViewPeopleInDifferentTables()
        {
            Viewer viewer = new Viewer();

            if (developers.Count > 0)
            {
                Console.WriteLine("Список разработчиков");
                viewer.ShowAllList(developers);
            }
            if (officeWorkers.Count > 0)
            {
                Console.WriteLine("Список сотрудников офиса");
                viewer.ShowAllList(officeWorkers);
            }
        }
    }
}
