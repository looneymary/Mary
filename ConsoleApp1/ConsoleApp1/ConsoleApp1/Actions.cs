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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WorkerViewer
{
    class Actions
    {
        public enum ActionsEnum { CreateWorker = 1, ShowWorkers = 2, ShowOneWorker = 3, SeachByAppoiintment = 4, SeachByName = 5, DeleteWorker = 6, QuitProgram = 7 };

        public enum ElementsOfXml
        {
            FirstName = 1, LastName = 2, Sex = 3, Appointment = 4, Date = 5, Salary = 6,
            DeveloperLanguage = 7, Experience = 8, Level = 9, YearsInService = 10
        }

        public delegate int ValidValuesDelegate(params string[] parametres);
        public static event ValidValuesDelegate СheckingValid;

        public delegate int ValidXmlValuesDelegate(string value, int i);
        public static event ValidXmlValuesDelegate СheckingXmlValid;

        private IRepository _repository;
        Viewer viewer = new Viewer();
        CheckValidExceptions ex = new CheckValidExceptions();
        WorkWithXml xml = new WorkWithXml();
        BusinessLayerMethods business;
        ValidXml valid = new ValidXml();

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

        public Actions()
        {
            this._repository = new XmlRepository();
            business = new BusinessLayerMethods(_repository);
        }

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
                            this._repository.Create(developer);
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
                            this._repository.Create(office);
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
            IEnumerable<Worker> workers = this._repository.Get("Workers/*");
            viewer.ShowAllList(workers);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("List of developers");
            Console.ForegroundColor = ConsoleColor.White;
            IEnumerable<Worker> devWorkers = this._repository.Get("Workers/Developer");
            viewer.ShowAllList(devWorkers);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("List of office workers");
            Console.ForegroundColor = ConsoleColor.White;
            IEnumerable<Worker> officeWorkers = this._repository.Get("Workers/OfficeWorker");
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

            if (this._repository.Get("Workers/*[" + index + "]").Count() > 0)
            {
                foreach (var person in this._repository.Get("Workers/*[" + index + "]"))
                {
                    worker._id = person._id;
                    this._repository.Delete(worker._id);
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
            Developer dev = new Developer();
            OfficeWorker office = new OfficeWorker();

            string newValue;

            Console.WriteLine("Enter the worker's index number: ");
            int index = int.Parse(Console.ReadLine());
            
            СheckingXmlValid += ex.CheckXmlExeptions;
            
            IEnumerable<Worker> workers = this._repository.Get("Workers/*[" + index + "]");
            if (workers.Count() == 1)
            {
                foreach (var worker in workers)
                {
                    Console.WriteLine("Enter new value or press \"Enter\" to continue;");
                        
                    if (business.IsDeveloper(worker))
                    {
                        dev = (Developer)worker;
                        office = (OfficeWorker)worker;
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == 0) continue;

                            Console.WriteLine(((ElementsOfXml)i).ToString());
                            if (i == 3) Console.WriteLine("Male/Female");
                            newValue = ti.ToTitleCase(Console.ReadLine());
                            if (newValue.Length > 0)
                            {
                                СheckingXmlValid(newValue, i);
                                if (ex.ValidResult == 0)
                                {
                                    if (i == 1) dev.FirstName = newValue;
                                    if (i == 2) dev.LastName = newValue;
                                    if (i == 4) dev.Appointment = newValue;
                                    if (i == 5) dev.Date = newValue;
                                    if (i == 6) dev.Salary = int.Parse(newValue);
                                    if (i == 7) dev.DevLang = newValue;
                                    if (i == 6) dev.Experience = newValue;
                                    if (i == 7) dev.Level = newValue;
                                    if (i == 3)
                                    {
                                        if (newValue == "Male")
                                        {
                                            office.Sex = (EnumsForModels.TypeOfSex)1;
                                        }
                                        else if (i == 3 && newValue == "Female")
                                        {
                                            office.Sex = (EnumsForModels.TypeOfSex)2;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect value");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        this._repository.Update(dev);
                    }                    
                    else
                    {
                        office = (OfficeWorker)worker;
                        for (int i = 0; i < 8; i++)
                        {
                            if (i == 0) continue;
                            if (i == 7) i = 10;
                                
                            Console.WriteLine(((ElementsOfXml)i).ToString());
                            if (i == 3) Console.WriteLine("Male/Female");
                            newValue = ti.ToTitleCase(Console.ReadLine());
                            if (newValue.Length > 0)
                            {
                                    
                                СheckingXmlValid(newValue, i);
                                if (ex.ValidResult == 0)
                                {
                                    if (i == 1) office.FirstName = newValue;
                                    if (i == 2) office.LastName = newValue;                                        
                                    if (i == 4) office.Appointment = newValue;
                                    if (i == 5) office.Date = newValue;
                                    if (i == 6) office.Salary = int.Parse(newValue);
                                    if (i == 7) office.YearsInService = int.Parse(newValue);
                                    if (i == 3)
                                    {
                                        if (newValue == "Male")
                                        {
                                            office.Sex = (EnumsForModels.TypeOfSex)1;
                                        }
                                        else if (i == 3 && newValue == "Female")
                                        {
                                            office.Sex = (EnumsForModels.TypeOfSex)2;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect value");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }                                
                        }
                        this._repository.Update(office);
                    }
                }
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