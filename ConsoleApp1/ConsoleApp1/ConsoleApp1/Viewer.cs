using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;
using ClassLibrary;

namespace ConsoleApp1
{
    class Viewer
    {
        /// <summary>
        /// Show all workers in table
        /// </summary>
        /// <param name="people">The list of all workers</param>
        public void ShowAllList(List<Worker> people)
        {
            string tableHeader = string.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 8} | {4, 11} | {5, 29} | {6, 10} |", 
                "№", "Last name", "First name", "Sex", "Appointment", "Date of taking office", "Salary");
            Console.WriteLine(tableHeader);
            foreach (var person in people.OrderBy(person => person._id))
            {
                Console.WriteLine(String.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 8} | {4, 11} | {5, 29} | {6, 10} |",
                    person._id, person.LastName, person.FirstName, person.Sex, person.Appointment, person.Date, person.Salary));
            }
        }        
    }
}
