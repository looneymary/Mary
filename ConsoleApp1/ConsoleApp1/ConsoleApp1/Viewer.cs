using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    class Viewer
    {
        // Вывести всех.
        public void ShowAllList(List<Repository> people)
        {
            int i = 1;
            string TableHeader = string.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 8} | {4, 10} | {5, 29} | {6, 10} |", 
                "№", "Фамилия", "Имя", "Пол", "Должность", "Дата вступления в должность", "Оклад");
            Console.WriteLine(TableHeader);
            foreach (var person in people)
            {
                Console.WriteLine(String.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 8} | {4, 10} | {5, 29} | {6, 10} |",
                    i, person.LastName, person.FirstName, person.Sex, person.Appointment, person.Date, person.Salary));
                i++;
            }
        }        
    }
}
