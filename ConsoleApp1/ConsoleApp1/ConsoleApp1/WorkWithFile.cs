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
    class WorkWithFile
    {
        public WorkWithFile()
        {
        }
    
        // Запись в файл.
        public void WriteToFile(List<Worker> people)
        {
            Repository repository = new Repository();
            string TextToWriteInAFile = "";
           
            foreach (var person in people)
            {
                if (repository.IsDeveloper(person))
                {
                    TextToWriteInAFile += "Developer: " + person.ToString() + "\r\n";
                }
                else
                {
                    TextToWriteInAFile += "OfficeWorker: " + person.ToString() + "\r\n";
                }
            }
            File.WriteAllText(Config._filePath, TextToWriteInAFile);
            Console.WriteLine("Запись данных прошла успешно.");
        }

        // Чтение из файла.
        public virtual void ReadFromFile(List<Worker> people)
        {
            people.Clear();

            foreach (var line in File.ReadLines(Config._filePath))
            {
                string[] names = line.Split(' ');

                string firstName = names[1];
                string lastName = names[2];
                EnumsForModels.TypeOfSex sex = (EnumsForModels.TypeOfSex)Enum.Parse(typeof(EnumsForModels.TypeOfSex), names[3]);
                string appointment = names[4];
                string date = names[5];
                int salary = int.Parse(names[6]);
                
                if(names[0] == "Developer:")
                {
                    Developer developer = new Developer(firstName, lastName, sex, appointment, date, salary);
                    people.Add(developer);
                }
                if(names[0] == "OfficeWorker:")
                {
                    OfficeWorker office = new OfficeWorker(firstName, lastName, sex, appointment, date, salary);
                    people.Add(office);
                }
            }
            Console.WriteLine("Чтение данных прошло успешно.");
        }

        // Add person to file
        public void AddPersonToFile(Object obj)
        {
            Repository rep = new Repository();
            string[] stringCount = File.ReadAllLines(Config._filePath);
            int length = stringCount.Length;
            string TextToWriteInAFile = "";
            if (stringCount.Count() != 0)
            {
                stringCount[length-1] += System.Environment.NewLine;
            }
            if (rep.IsDeveloper(obj) == true)
            {
                TextToWriteInAFile += "Developer: ";
            }
            else
            {
                TextToWriteInAFile += "OfficeWorker: ";
            }
            TextToWriteInAFile += obj.ToString();
            File.AppendAllText(Config._filePath, TextToWriteInAFile);
            Console.WriteLine("Запись данных прошла успешно.");
        }
    }
}
