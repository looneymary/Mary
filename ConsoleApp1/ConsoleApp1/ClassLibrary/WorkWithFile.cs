using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using ClassLibrary.Models;

namespace ClassLibrary
{
   public class WorkWithFile
    {
        public WorkWithFile()
        {
        }
    
        // Запись в файл.
        public void WriteToFile(List<Worker> people)
        {
            Repository repository = new Repository();
            string textToWriteInAFile = "";
           
            foreach (var person in people)
            {
                if (repository.IsDeveloper(person))
                {
                    textToWriteInAFile += "Developer: " + person.ToString() + "\r\n";
                }
                else
                {
                    textToWriteInAFile += "OfficeWorker: " + person.ToString() + "\r\n";
                }
            }
            File.WriteAllText(Config._filePath, textToWriteInAFile);
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
                    string devLang = names[7];
                    string experience = names[8];
                    string level = names[9];
                    Developer developer = new Developer(firstName, lastName, sex, appointment, date, salary, devLang, experience, level);
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
            string textToWriteInAFile = "";
            if (stringCount.Count() != 0)
            {
                textToWriteInAFile += System.Environment.NewLine;
            }
            if (rep.IsDeveloper(obj) == true)
            {
                textToWriteInAFile += "Developer: ";
            }
            else
            {
                textToWriteInAFile += "OfficeWorker: ";
            }
            textToWriteInAFile += obj.ToString();
            File.AppendAllText(Config._filePath, textToWriteInAFile);
            Console.WriteLine("Запись данных прошла успешно.");
        }
    }
}
