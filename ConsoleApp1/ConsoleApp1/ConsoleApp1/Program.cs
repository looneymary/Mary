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

        public delegate int ValidWorkerEnumDelegate(string ValidWorkerEnum);
        public static event ValidWorkerEnumDelegate ValidWorkerEnum;

        public delegate int ValidSexEnumDelegate(string ValidEnums);
        public static event ValidSexEnumDelegate ValidSexEnum;

        public static void Main(string[] args)
        {
            int i;
            Repository repository = new Repository();
            Viewer viewer = new Viewer();
            CheckValidExceptions ex = new CheckValidExceptions();
            ValidEnum valid = new ValidEnum();


            try
            {
                do
                {
                    Console.Write("Menu:\n1. Add info about a worker \n2. Show info about all workers \n" +
                                        "3. Show info about one worker \n4. Searching by appointment \n" +
                                        "5. Counting by appointment \n6. Delete worker \n7. Quite the program\n\n");

                    i = int.Parse(Console.ReadLine());
                    switch (i)
                    {
                        // Add info about worker.
                        case 1:

                            EnumsForModels.WorkerType workerType;
                            СheckingValid += ex.CheckExceptions;
                            ValidWorkerEnum += valid.ValidEnums<EnumsForModels.WorkerType>;
                            ValidSexEnum += valid.ValidEnums<EnumsForModels.TypeOfSex>;

                            Console.WriteLine("Information: \n");

                            Console.WriteLine("Select type:\n1). Developer \n2). Office worker \n ");
                            workerType = (EnumsForModels.WorkerType)int.Parse(Console.ReadLine());
                            int resultValid = ValidWorkerEnum(workerType.ToString());
                            if (resultValid == 1)
                            {
                                try
                                {
                                    int id = repository.AddIndexNumber();

                                    Console.WriteLine("First name:");
                                    string firstName = Console.ReadLine();

                                    Console.WriteLine("Last name:");
                                    string lastName = Console.ReadLine();

                                    Console.WriteLine("Sex: m - 1/ f - 2.");

                                    EnumsForModels.TypeOfSex sex = (EnumsForModels.TypeOfSex)int.Parse(Console.ReadLine());
                                    resultValid = ValidSexEnum(sex.ToString());
                                    if (resultValid == 1)
                                    {
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
                                            СheckingValid(firstName, lastName, sex.ToString(), appointment, date, salary.ToString());
                                            if (ex.ValidResult == 0)
                                            {
                                                OfficeWorker office = new OfficeWorker(id, firstName, lastName, sex, appointment, date, salary);
                                                repository.AddWorker(office);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect data was entered.");
                                        }
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Incorrect data was entered.");
                                }
                            }
                            break;
                        // List all workers.
                        case 2:
                            List<Worker> workers = repository.GetList();
                            repository.GetWorkersFromFile();
                            Console.WriteLine("Workers");
                            viewer.ShowAllList(workers);
                            var dev = repository.DeveloperWorkers();
                            if (dev.Count() > 0)
                            {
                                Console.WriteLine("List of developers");
                                viewer.ShowAllList(repository.DeveloperWorkers());
                            }
                            var officeWorkers = repository.OfficeWorkers();
                            if (officeWorkers.Count() > 0)
                            {
                                Console.WriteLine("List of office workers");
                                viewer.ShowAllList(repository.OfficeWorkers());
                            }
                            break;
                        // Show info about one worker.
                        case 3:
                            Console.WriteLine("Enter the index number:");
                            int indexNumber = int.Parse(Console.ReadLine());
                            string onePerson = repository.ShowOnePerson(indexNumber);
                            Console.WriteLine(onePerson);
                            break;
                        // Searching by appointment.
                        case 4:
                            Console.WriteLine("Enter an appointment: ");
                            string searchAppointment = Console.ReadLine();
                            string result = repository.SearchByAppointment(searchAppointment);
                            Console.WriteLine(result);
                            break;
                        // Counting by appointment.
                        case 5:
                            Console.WriteLine("Enter an appointment:");
                            string countAppointment = Console.ReadLine();
                            string countResult = repository.CountWorkers(countAppointment);
                            Console.WriteLine(countResult);
                            break;
                        // Delete worker.
                        case 6:
                            Console.WriteLine("Enter the worker's index number: ");
                            int number = int.Parse(Console.ReadLine());
                            string removeResult = repository.RemovePerson(number);
                            Console.WriteLine(removeResult);
                            break;
                        // Quite the program.
                        case 7:
                            Console.WriteLine("Quite the program.");
                            break;
                        default:
                            Console.WriteLine("There is no such item in menu.");
                            break;
                    }
                    Console.Write("\n\n\t\t\tReturn to main menu...");
                    Console.ReadLine();
                    Console.Clear();

                }
                while (i != 7);
            }
            catch (System.FormatException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
