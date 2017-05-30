using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    class Params
    {
        private string filePath;
        private ArrayList fullInfoArr;
        string fullInfo;

        public Params()
        {
            //Путь к файлу
            filePath = string.Format("{0} spisok.txt", AppDomain.CurrentDomain.BaseDirectory);

            //Коллекция строк со всей инфой для записи в файл
            fullInfoArr = new ArrayList();
        }

        public void AddInfo()
        {
            Console.WriteLine("Информация о сотруднике: \n");

            Console.WriteLine("Имя:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Фамилия:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Пол (м/ж):");
            string sex = Console.ReadLine();

            Console.WriteLine("Должность:");
            string appointment = Console.ReadLine();

            Console.WriteLine("\nРабочая информация: \n");

            Console.WriteLine("Дата вступления в должность:");
            string date = Console.ReadLine();

            Console.WriteLine("Оклад:");
            string val = Console.ReadLine();
            int salary = int.Parse(val);

            
            fullInfo = string.Format("| {0, 10} | {1, 10} | {2, 3} | {3, 10} | {4, 29} | {5, 10} |", lastName, firstName, sex, appointment, date, salary);

            //Добавить пользователя в коллекцию
            fullInfoArr.Add(fullInfo);
        }

        public void ShowAllList()
        {
            int i = 1;
            string shapka = string.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 3} | {4, 10} | {5, 29} | {6, 10} |", "№", "Фамилия", "Имя", "Пол", "Должность", "Дата вступления в должность", "Оклад");
            Console.WriteLine(shapka);
            foreach (var person in fullInfoArr)
            {
                Console.WriteLine(string.Format("| {0, 2} {1}", i, person));
                i++;
            }
        }
        
        public void ShowOnePerson()
        {
            int person;
            Console.WriteLine("Введите порядковый номер:");
            person = int.Parse(Console.ReadLine());
            Console.WriteLine(fullInfoArr[person - 1]);
        }

        public void WriteToFile()
        {
            string textToWriteInAFile = "";
            foreach (var a in fullInfoArr)
            {
                textToWriteInAFile += (string)a + ";\r\n";
            }
            File.WriteAllText(filePath, textToWriteInAFile);
            Console.WriteLine("Запись данных прошла успешно.");
        }

        public void ReadFromFile()
        {

            foreach (string line in File.ReadLines(filePath))
            {
                fullInfoArr.Add(line);
            }
            Console.WriteLine("Чтение данных прошло успешно.");
        }
    }
}
