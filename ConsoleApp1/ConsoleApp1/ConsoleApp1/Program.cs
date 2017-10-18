using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using DataAccess;
using DataAccess.Models;

namespace WorkerViewer
{
    class Program
    {        
        public static void Main(string[] args)
        {
            int typedEnumValue = 0;
            Actions actions = new Actions();

            do
            {                
                Console.Write("Menu:\n1. Add info about a worker \n2. Show info about all workers \n" +
                        "3. Show info about one worker \n4. Searching by appointment \n" +
                        "5. Counting by appointment \n6. Delete worker \n7. Update worker \n8. Quit the program\n\n");
                try
                {
                    typedEnumValue = (int)(Actions.ActionsEnum)int.Parse(Console.ReadLine());

                    switch (typedEnumValue)
                    {
                        // Add info about worker.
                        case 1:
                            actions.CreateWorker();
                        break;
                        // List all workers.
                        case 2:
                            actions.ShowWorkers();
                            break;
                        // Show info about one worker.
                        case 3:
                            actions.ShowOneWorker();
                            break;
                        // Searching by appointment.
                        case 4:
                            actions.SeachByAppoiintment();
                            break;
                        // Counting by appointment.
                        case 5:
                            actions.CountAppointment();
                            break;
                        // Delete worker.
                        case 6:
                            actions.DeleteWorker();
                            break;
                        // Update worker.
                        case 7:
                            actions.UpdateXml();
                            break;
                        // Quit the program.
                        case 8:
                            actions.QuitProgram();
                            break;
                        default:
                            Console.WriteLine("There is no such item in main menu.");
                            break;
                    }
                }   
                catch
                {
                    Console.WriteLine("Incorrect value was entered.");
                }
                Console.Write("\n\n\t\t\tReturn to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
            while (typedEnumValue != 8);
        }
    }
}
