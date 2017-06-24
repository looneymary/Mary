using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    class WorkWithFile : Repository
    {
        public WorkWithFile() : base()
        {
        }
    
        // Запись в файл.
        public void WriteToFile(List<Repository> developers, List<Repository> officeWorkers)
        {
            Repository rep = new Repository();

            string TextToWriteInAFile = "";
            foreach (var person in developers)
            {                
                TextToWriteInAFile += "Developer: " + person.ToString() + "\r\n";
            }
            foreach (var person in officeWorkers)
            {
                TextToWriteInAFile += "OfficeWorker: " + person.ToString() + "\r\n";
            }
            File.WriteAllText(Config._filePath, TextToWriteInAFile);
            Console.WriteLine("Запись данных прошла успешно.");
        }

        // Add person to file
        public void AddPersonToFile(Object obj)
        {
            Repository rep = new Repository();

            string[] stringCount = File.ReadAllLines(Config._filePath);
            string TextToWriteInAFile = "";
            if (stringCount.Count() != 0)
            {
                TextToWriteInAFile = System.Environment.NewLine;
            }
            if(rep.IsDeveloper(obj) == true)
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

        // Чтение из файла.
        public virtual void ReadFromFile(List<Repository> people, List<Repository> developers, List<Repository> officeWorkers)
        {
            foreach (var line in File.ReadLines(Config._filePath))
            {
                Repository repository = new Repository();

                string[] names = line.Split(' ');
                
                repository.LastName = names[1];
                repository.FirstName = names[2];
                repository.Sex = (EnumHelper.TypeOfSex)Enum.Parse(typeof(EnumHelper.TypeOfSex), names[3]);
                repository.Appointment = names[4];
                repository.Date = names[5];
                repository.Salary = int.Parse(names[6]);
                
                people.Add(repository);
                if(names[0] == "OfficeWorker:")
                {
                    officeWorkers.Add(repository);
                }
                else
                {
                    developers.Add(repository);
                }
            }
            Console.WriteLine("Чтение данных прошло успешно.");
        }
    }
}
