using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using DataAccess;
using DataAccess.Models;
using System.Globalization;
using BusinessLayer;

namespace WorkerViewer
{
    class Actions
    {
        public delegate int ValidValuesDelegate(params string[] parametres);
        public static event ValidValuesDelegate СheckingValid;

        public delegate int ValidXmlValuesDelegate(string value, int i);
        public static event ValidXmlValuesDelegate СheckingXmlValid;

        XmlRepository repository = new XmlRepository();
        Viewer viewer = new Viewer();
        CheckValidExceptions ex = new CheckValidExceptions();
        WorkWithXml xml = new WorkWithXml();
        BusinessLayerClass business = new BusinessLayerClass();

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

        public Actions()
        {
        }

        public enum ActionsEnum { CreateWorker = 1, ShowWorkers = 2, ShowOneWorker = 3, SeachByAppoiintment = 4, SeachByName = 5, DeleteWorker = 6, QuitProgram = 7 };

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

                Console.WriteLine("First name:");
                
                string firstName = ti.ToTitleCase(Console.ReadLine());

                Console.WriteLine("Last name:");
                string lastName = ti.ToTitleCase(Console.ReadLine());

                Console.WriteLine("Sex: m - 1/ f - 2.");

                int sexType = int.Parse(Console.ReadLine());

                bool isSexTypeDefined = Enum.IsDefined(typeof(EnumsForModels.TypeOfSex), sexType);
                if (isSexTypeDefined)
                {
                    EnumsForModels.TypeOfSex sex = (EnumsForModels.TypeOfSex)sexType;
                    Console.WriteLine("Appointment:");
                    string appointment = ti.ToTitleCase(Console.ReadLine());

                    Console.WriteLine("Working information: \n");

                    Console.WriteLine("The date of taking office :");
                    string date = ti.ToTitleCase(Console.ReadLine());

                    Console.WriteLine("Salary:");
                    int salary = int.Parse(Console.ReadLine());
                    if (workerType == EnumsForModels.WorkerType.Developer)
                    {
                        Console.WriteLine("Development languages:");
                        string devLang = ti.ToTitleCase(Console.ReadLine());

                        Console.WriteLine("Experience:");
                        string experience = Console.ReadLine();

                        Console.WriteLine("Level:");
                        string level = ti.ToTitleCase(Console.ReadLine());

                        СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary.ToString(), devLang,
                                      experience, level);

                        if (ex.ValidResult == 0)
                        {
                            Developer developer = new Developer(firstName, lastName, sex, appointment, date, salary,
                                                            devLang, experience, level);
                            repository.Create(developer);
                        }
                    }
                    else if (workerType == EnumsForModels.WorkerType.OfficeWorker)
                    {
                        Console.WriteLine("How many years in service:");
                        int yearsInService = int.Parse(Console.ReadLine());

                        СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary.ToString(), yearsInService.ToString());
                        if (ex.ValidResult == 0)
                        {
                            OfficeWorker office = new OfficeWorker(firstName, lastName, sex, appointment, date, salary, yearsInService);
                            repository.Create(office);
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Workers");
            IEnumerable<Worker> workers = repository.Get("Workers/*");
            viewer.ShowAllList(workers);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("List of developers");
            Console.ForegroundColor = ConsoleColor.White;
            IEnumerable<Worker> devWorkers = repository.Get("Workers/Developer");
            viewer.ShowAllList(devWorkers);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("List of office workers");
            Console.ForegroundColor = ConsoleColor.White;
            IEnumerable<Worker> officeWorkers = repository.Get("Workers/OfficeWorker");
            viewer.ShowAllList(officeWorkers);
        }

        /// <summary>
        /// Show info about one worker (case 3)
        /// </summary>
        public void ShowOneWorker()
        {
            Console.WriteLine("Enter the index number:");
            int indexNumber = int.Parse(Console.ReadLine());
            business.ShowOnePerson(indexNumber);            
        }

        /// <summary>
        /// Searching by appointment (case 4)
        /// </summary>
        public void SeachByAppoiintment()
        {
            Console.WriteLine("Enter an appointment: ");
            string searchAppointment = ti.ToTitleCase(Console.ReadLine());
            business.SearchByAppointment(searchAppointment);
        }

        /// <summary>
        /// Counting by appointment (case 5)
        /// </summary>
        public void CountAppointment()
        {
            Console.WriteLine("Enter an appointment:");
            string countAppointment = ti.ToTitleCase(Console.ReadLine());
            Console.WriteLine(business.CountWorkers(countAppointment));
        }

        /// <summary>
        /// Delete worker (case 6)
        /// </summary>
        public void DeleteWorker()
        {
            Worker worker = new Developer();
            Console.WriteLine("Enter the worker's index number: ");
            int index = int.Parse(Console.ReadLine());

            if (repository.Get("Workers/*[" + index + "]").Count() > 0)
            {
                foreach (var person in repository.Get("Workers/*[" + index + "]"))
                {
                    worker._id = person._id;
                    repository.Delete(worker._id);
                }
            }
            else
            {
                Console.WriteLine("Incorrect index");
            }
        }

        /// <summary>
        /// Update value in xml-document
        /// </summary>
        public void UpdateXml()
        {
            СheckingXmlValid += ex.CheckXmlExeptions;
            Worker worker = new Developer();

            Console.WriteLine("Enter the worker's index number: ");
            int index = int.Parse(Console.ReadLine());

            if (repository.Get("Workers/*[" + index + "]").Count() > 0)
            {
                foreach (var person in repository.Get("Workers/*[" + index + "]"))
                {
                    worker._id = person._id;
                }
            }
            else
            {
                Console.WriteLine("Incorrect index");
            }

            Console.WriteLine("Chose element what you want to update.");
            Console.WriteLine("1 - first name\n2 - last name\n3 - sex: Male/Female \n4 - appointment\n5 - date\n6 - salary");
            Console.WriteLine();
            Console.WriteLine("For developers:\n7 - developer language\n8 - experiience\n9 - level");
            Console.WriteLine();
            Console.WriteLine("For office workers:\n10 - years in service");
            int numOfElement = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new value:");
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            string value = ti.ToTitleCase(Console.ReadLine());

            СheckingXmlValid(value, numOfElement);
            if (ex.ValidResult == 0)
            {
                xml.UpdateXml(Config._xmlPath, worker._id, numOfElement, value);
            }
        }

        /// <summary>
        /// Quit the program (case 7)
        /// </summary>
        public void QuitProgram()
        {
            Console.WriteLine("Quite the program.");
        }
    }
}