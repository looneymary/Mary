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
    class Actions
    {
        public enum ActionsEnum { CreateWorker = 1, ShowWorkers = 2, ShowOneWorker = 3, SeachByAppoiintment = 4, SeachByName = 5, DeleteWorker = 6, QuitProgram = 7 };

        public delegate int ValidValuesDelegate(params string[] parametres);
        public static event ValidValuesDelegate СheckingValid;

        Repository repository = new Repository();
        Viewer viewer = new Viewer();
        CheckValidExceptions ex = new CheckValidExceptions();

        /// <summary>
        /// Add info about worker (case 1)
        /// </summary>
        public void CreateWorker()
        {
            EnumsForModels.WorkerType workerType;
            СheckingValid += ex.CheckExceptions;

            Console.WriteLine("Information: \n");

            Console.WriteLine("Select type:\n1). Developer \n2). Office worker \n ");

            int workerEnum = int.Parse(Console.ReadLine());

            bool isWorkerTypeDefined = Enum.IsDefined(typeof(EnumsForModels.TypeOfSex), workerEnum);

            if (isWorkerTypeDefined)
            {
                workerType = (EnumsForModels.WorkerType)workerEnum;
                int id = repository.AddIndexNumber();

                Console.WriteLine("First name:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Last name:");
                string lastName = Console.ReadLine();

                Console.WriteLine("Sex: m - 1/ f - 2.");

                int sexType = int.Parse(Console.ReadLine());

                bool isSexTypeDefined = Enum.IsDefined(typeof(EnumsForModels.TypeOfSex), sexType);
                if (isSexTypeDefined)
                {
                    EnumsForModels.TypeOfSex sex = (EnumsForModels.TypeOfSex)sexType;
                    Console.WriteLine("Appointment:");
                    string appointment = Console.ReadLine();

                    Console.WriteLine("Working information: \n");

                    Console.WriteLine("The date of taking office :");
                    string date = Console.ReadLine();

                    Console.WriteLine("Salary:");
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
                            Developer developer = new Developer(id, firstName, lastName, sex, appointment, date, salary,
                                                            devLang, experience, level);
                            repository.AddWorker(developer);
                        }
                    }
                    else if (workerType == EnumsForModels.WorkerType.OfficeWorker)
                    {
                        Console.WriteLine("How many years in service:");
                        int yearsInService = int.Parse(Console.ReadLine());

                        СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary.ToString(), yearsInService.ToString());
                        if (ex.ValidResult == 0)
                        {

                            OfficeWorker office = new OfficeWorker(id, firstName, lastName, sex, appointment, date, salary, yearsInService);
                            repository.AddWorker(office);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There is no such item in menu.");
                }
            }
            else
            {
                Console.WriteLine("There is no such item in menu.");
            }
        }

        /// <summary>
        /// List all workers (case 2)
        /// </summary>
        public void ShowWorkers()
        {
            List<Worker> workers = repository.GetList();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Workers");
            //Console.ForegroundColor = ConsoleColor.White;
            viewer.ShowAllList(workers);
            var dev = repository.DeveloperWorkers();
            if (dev.Count() > 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("List of developers");
                Console.ForegroundColor = ConsoleColor.White;
                viewer.ShowAllList(repository.DeveloperWorkers());
            }
            var officeWorkers = repository.OfficeWorkers();
            if (officeWorkers.Count() > 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("List of office workers");
                Console.ForegroundColor = ConsoleColor.White;
                viewer.ShowAllList(repository.OfficeWorkers());
            }
        }

        /// <summary>
        /// Show info about one worker (case 3)
        /// </summary>
        public void ShowOneWorker()
        {
            Console.WriteLine("Enter the index number:");
            int indexNumber = int.Parse(Console.ReadLine());
            string onePerson = repository.ShowOnePerson(indexNumber);
            Console.WriteLine(onePerson);
        }

        /// <summary>
        /// Searching by appointment (case 4)
        /// </summary>
        public void SeachByAppoiintment()
        {
            Console.WriteLine("Enter an appointment: ");
            string searchAppointment = Console.ReadLine();
            string result = repository.SearchByAppointment(searchAppointment);
            Console.WriteLine(result);
        }

        /// <summary>
        /// Counting by appointment (case 5)
        /// </summary>
        public void SeachByName()
        {
            Console.WriteLine("Enter an appointment:");
            string countAppointment = Console.ReadLine();
            string countResult = repository.CountWorkers(countAppointment);
            Console.WriteLine(countResult);
        }

        /// <summary>
        /// Delete worker (case 6)
        /// </summary>
        public void DeleteWorker()
        {
            Console.WriteLine("Enter the worker's index number: ");
            int number = int.Parse(Console.ReadLine());
            string removeResult = repository.RemovePerson(number);
            Console.WriteLine(removeResult);
        }

        /// <summary>
        /// Quit the program (case 7)
        /// </summary>
        public void QuitProgram()
        {
            Console.WriteLine("Quite the program.");
        }

        public void Valid()
        {
            ValidXml valid = new ValidXml();
            valid.Validate(Config._xmlPath, Config._xsdPath);
        }
    }
}
