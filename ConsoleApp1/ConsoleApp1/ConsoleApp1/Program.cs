using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using ClassLibrary;
using ClassLibrary.Models;


namespace ConsoleApp1
{
    class Program
    {
        public delegate int ValidValuesDelegate(params string[] parametres);
        public static event ValidValuesDelegate СheckingValid;

        public static void Main(string[] args)
        {
            int i;
            Repository repository = new Repository();
            WorkWithFile file = new WorkWithFile();
            Viewer viewer = new Viewer();
            CheckValidExceptions ex = new CheckValidExceptions();

            do
            {
                Console.Write("Меню:\n1. Ввести данные о сотруднике \n2. Вывести информацию о всех сотрудниках \n" +
                                    "3. Вывести информацию об одном сотруднике\n4. Поиск по должности \n" +
                                    "5. Подсчёт по должности \n6. Удалить сотрудника \n7. Выйти из программы\n\n");

                i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    // Добавить информацию о сотруднике.
                    case 1:

                        EnumsForModels.WorkerType workerType;
                        СheckingValid += ex.CheckExceptions;

                        Console.WriteLine("Информация о сотруднике: \n");

                        Console.WriteLine("Укажите тип:\n1). Разработчик \n2). Работник офиса \n ");
                        workerType = (EnumsForModels.WorkerType)int.Parse(Console.ReadLine());
                        try
                        {
                            Console.WriteLine("Имя:");
                            string firstName = Console.ReadLine();

                            Console.WriteLine("Фамилия:");
                            string lastName = Console.ReadLine();

                            Console.WriteLine("Пол: м - 1/ ж - 2.");

                            EnumsForModels.TypeOfSex sex = (EnumsForModels.TypeOfSex)int.Parse(Console.ReadLine());

                            Console.WriteLine("Должность:");
                            string appointment = Console.ReadLine();

                            Console.WriteLine("\nРабочая информация: \n");

                            Console.WriteLine("Дата вступления в должность:");
                            string date = Console.ReadLine();

                            Console.WriteLine("Оклад:");
                            int salary = int.Parse(Console.ReadLine());

                            if (workerType == EnumsForModels.WorkerType.Developer)
                            {
                                Console.WriteLine("Development languages:");
                                string devLang = Console.ReadLine();

                                Console.WriteLine("Experience:");
                                string experience = Console.ReadLine();

                                Console.WriteLine("Level:");
                                string level = Console.ReadLine();

                                СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary.ToString(), devLang, 
                                              experience, level);

                                if (ex.ValidResult == 0)
                                {
                                    Developer developer = new Developer(firstName, lastName, sex, appointment, date, salary,
                                                                        devLang, experience, level);
                                    repository.AddWorker(developer);                                    
                                }
                            }
                            else if (workerType == EnumsForModels.WorkerType.OfficeWorker)
                            {
                                СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary.ToString());
                                if (ex.ValidResult == 0)
                                {
                                    OfficeWorker office = new OfficeWorker(firstName, lastName, sex, appointment, date, salary);
                                    repository.AddWorker(office);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Введены неверные данные");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Введены неверные данные.");
                        }
                        break;
                    // Вывести список всех сотрудников.
                    case 2:
                        repository.GetWorkersFromFile();
                        Console.WriteLine("Workers");
                        viewer.ShowAllList(repository.people);
                        var dev = repository.DeveloperWorkers(repository.people);
                        if(dev.Count() > 0)
                        {
                            Console.WriteLine("List of developers");
                            viewer.ShowAllList(repository.DeveloperWorkers(repository.people));
                        }
                        var officeWorkers = repository.OfficeWorkers(repository.people);
                        if (officeWorkers.Count() > 0)
                        {
                            Console.WriteLine("List of office workers");
                            viewer.ShowAllList(repository.OfficeWorkers(repository.people));
                        }
                        break;
                    // Вывести информацию об одном сотруднике.
                    case 3:
                        Console.WriteLine("Введите порядковый номер:");
                        int indexNumber = int.Parse(Console.ReadLine());
                        string onePerson = repository.ShowOnePerson(repository.people, indexNumber);
                        Console.WriteLine(onePerson);
                        break;                    
                    // Поиск по должности.
                    case 4:
                        Console.WriteLine("Введите должность: ");
                        string searchAppointment = Console.ReadLine();
                        string result = repository.SearchByAppointment(repository.people, searchAppointment);
                        Console.WriteLine(result);
                        break;
                    // Подсчёт по должности.
                    case 5:
                        Console.WriteLine("Enter the appointment:");
                        string countAppointment = Console.ReadLine();
                        string countResult = repository.CountWorkers(repository.people, countAppointment);
                        Console.WriteLine(countResult);
                        break;
                    // Удалить сотрудника.
                    case 6:
                        Console.WriteLine("Введите номер сотрудника: ");
                        int number = int.Parse(Console.ReadLine());
                        string removeResult = repository.RemovePerson(repository.people, number);
                        Console.WriteLine(removeResult);
                        break;
                    // Поиск по должности.
                    case 7:
                        Console.WriteLine("Закрыть приложение");
                        break;
                    default:
                        Console.WriteLine("Такого элемента нет в списке меню");
                        break;
                }
                Console.Write("\n\n\t\t\tВернуться к главному меню...");
                Console.ReadLine();
                Console.Clear();
            }
            while (i != 7);
        }            
    }
}
