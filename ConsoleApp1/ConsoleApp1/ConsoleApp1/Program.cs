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
        public delegate int ValidValuesDelegate(string a, string b, string c, string d, string f, int g);
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
                                    "3. Вывести информацию об одном сотруднике\n4. Запись в файл \n5. Чтение из файла\n" +
                                    "6. Поиск по должности \n7. Подсчёт по должности \n8. Удалить сотрудника \n9. Выйти из программы\n\n");

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
                                Console.WriteLine("Enter development languages:");
                                string devLang = Console.ReadLine();

                                Console.WriteLine("Enter experience:");
                                string experience = Console.ReadLine();

                                Console.WriteLine("Enter level:");
                                string level = Console.ReadLine();

                                СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary);

                                if (ex.ValidResult == 0)
                                {
                                    Developer developer = new Developer(firstName, lastName, sex, appointment, date, salary);
                                    //devLang, experience, level
                                    repository.AddWorker(developer);
                                    file.AddPersonToFile(developer);
                                }
                            }
                            else if (workerType == EnumsForModels.WorkerType.OfficeWorker)
                            {
                                СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary);
                                if (ex.ValidResult == 0)
                                {
                                    OfficeWorker office = new OfficeWorker(firstName, lastName, sex, appointment, date, salary);
                                    repository.AddWorker(office);
                                    file.AddPersonToFile(office);
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
                        file.ReadFromFile(repository.people);
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
                        repository.ShowOnePerson(repository.people);
                        break;
                    // Запись списка в файл.
                    case 4:
                        file.WriteToFile(repository.people);
                        break;
                    // Чтение данных из файла.
                    case 5:
                        file.ReadFromFile(repository.people);
                        break;
                    // Поиск по должности.
                    case 6:
                        repository.SearchByAppointment(repository.people);
                        break;
                    // Подсчёт по должности.
                    case 7:
                        repository.CountWorkers(repository.people);
                        break;
                    // Удалить сотрудника.
                    case 8:
                        repository.RemovePerson(repository.people);
                        file.WriteToFile(repository.people);
                        break;
                    // Поиск по должности.
                    case 9:
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
            while (i != 9);
        }            
    }
}
