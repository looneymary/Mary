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
    
        Config config = new Config();

        // Запись в файл.
        public void WriteToFile(List<Repository> people)
        {
            string TextToWriteInAFile = "";
            foreach (var a in people)
            {
                TextToWriteInAFile += a.ToString() + "\r\n";
            }
            File.WriteAllText(config.FilePath, TextToWriteInAFile);
            Console.WriteLine("Запись данных прошла успешно.");
        }

        // Чтение из файла.
        public virtual void ReadFromFile(List<Repository> people)
        {
            foreach (var line in File.ReadLines(config.FilePath))
            {
                Repository repository = new Repository();

                string[] names = line.Split(' ');

                repository.LastName = names[0];
                repository.FirstName = names[1];
                repository.Sex = names[2];
                repository.Appointment = names[3];
                repository.Date = names[4];
                repository.Salary = int.Parse(names[5]);

                people.Add(repository);
            }
            Console.WriteLine("Чтение данных прошло успешно.");
        }
    }
}
