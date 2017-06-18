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

        public Repository() : base()
        {
        }
        // Добавить.
        public virtual void AddInfo()
        {
            Console.WriteLine("Имя:");
            FirstName = Console.ReadLine();
            
            Console.WriteLine("Фамилия:");
            LastName = Console.ReadLine();

            Console.WriteLine("Пол (м/ж):");
            Sex = Console.ReadLine();

            Console.WriteLine("Должность:");
            Appointment = Console.ReadLine();

            Console.WriteLine("\nРабочая информация: \n");

            Console.WriteLine("Дата вступления в должность:");
            Date = Console.ReadLine();

            Console.WriteLine("Оклад:");
            string Val = Console.ReadLine();
            Salary = int.Parse(Val);
        }

        // Поиск.
        public virtual void SearchInfo(List<Repository> people)
        {
            Console.WriteLine("Введите должность: ");
            string SearchAppointment = Console.ReadLine();
            var result = people.FindAll(x => (x.Appointment.Contains(SearchAppointment)));
            if (result.Count > 0)
            {
                foreach (var res in result)
                {
                    Console.WriteLine(string.Format("{0} {1}", res.LastName, res.FirstName));
                }
            }
            else
            {
                Console.WriteLine("Поиск не дал результатов.");
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
            var result = people.FindAll(x => (x.Appointment.Contains(CountAppointment)));
            Console.WriteLine("{0} : {1}", CountAppointment, result.Count);
        }
        
        // Является ли разработчиком.
        public void IsDeveloper(Object obj)
        {
            Repository repository = new Repository();
            Console.WriteLine("Является ли сотрудник разработчиком?");
            bool val = obj is Developer;
        }
        
        // Удаление сотрудника.
        public void RemovePerson()
        {
            Console.WriteLine("Введите номер сотрудника: ");
            int number = int.Parse(Console.ReadLine());
            number -= 1;
            people.RemoveAt(number);
            Console.WriteLine("Сотрудник удалён");
        }
    }
}
