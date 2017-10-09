using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess;

namespace WorkerViewer
{
    class Viewer
    {
        /// <summary>
        /// Show all workers in table
        /// </summary>
        /// <param name="people">The list of all workers</param>
        public void ShowAllList(IEnumerable<Worker> workers)
        {
            int i = 1;

            string tableHeader = string.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 8} | {4, 20} | {5, 29} | {6, 10} |", 
                "№", "First name", "Last name", "Sex", "Appointment", "Date of taking office", "Salary");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(tableHeader);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var person in workers)
            {
                Console.WriteLine(String.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 8} | {4, 20} | {5, 29} | {6, 10} |",
                    i, person.FirstName, person.LastName, person.Sex, person.Appointment, person.Date, person.Salary));

                i++;
            }
        }        
    }
}
