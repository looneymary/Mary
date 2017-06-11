using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    class Methods : Worker
    {
        public string str;
        public int num;
        public int person;
        public List<Methods> people = new List<Methods>();

        public Methods() : base()
        {            
        }
        
        //Добавить
        public virtual void AddInfo()
        {
            Console.WriteLine("Имя:");
            FirstName = Console.ReadLine();

            Console.WriteLine("Фамилия:");
            LastName = Console.ReadLine();

            Console.WriteLine("Пол (м/ж):");
            Sex = Console.ReadLine();

            Console.WriteLine("Должность:");
            Appointment = Console.ReadLine();

            Console.WriteLine("\nРабочая информация: \n");

            Console.WriteLine("Дата вступления в должность:");
            Date = Console.ReadLine();

            Console.WriteLine("Оклад:");
            string val = Console.ReadLine();
            Salary = int.Parse(val);
        }

        //Вывести всех
        public void ShowAllList()
        {
            int i = 1;
            string shapka = string.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 3} | {4, 10} | {5, 29} | {6, 10} |", "№", "Фамилия", "Имя", "Пол", "Должность", "Дата вступления в должность", "Оклад");
            Console.WriteLine(shapka);
            foreach (var person in people)
            {
                Console.WriteLine(String.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 3} | {4, 10} | {5, 29} | {6, 10} |",
                    i, person.LastName, person.FirstName, person.Sex, person.Appointment, person.Date, person.Salary));
                i++;
            }
        }

        //Показать одного сотрудника
        public virtual void ShowOnePerson( )
        {            
            Console.WriteLine("Введите порядковый номер:");
            num = int.Parse(Console.ReadLine());
            str = String.Format("  {0} {1}, пол: {2}, должность: {3}, дата вступления в должность: {4}, оклад: {5}  ", people[num - 1].LastName, people[num - 1].FirstName,
                people[num - 1].Sex, people[num - 1].Appointment, people[num - 1].Date, people[num - 1].Salary);
            Console.WriteLine(str);
        }

        //Запись в файл
        public void WriteToFile()
        {
            string textToWriteInAFile = "";
            foreach (var a in people)
            {
                textToWriteInAFile += a.ToString() + "\r\n";
            }
            File.WriteAllText(FilePath, textToWriteInAFile);
            Console.WriteLine("Запись данных прошла успешно.");
        }

        //Чтение из файла
        public virtual void ReadFromFile()
        {
            foreach (var line in File.ReadLines(FilePath))
            {
                Methods methods1 = new Methods();
                string[] names = line.Split(' ');
                methods1.LastName = names[0];
                methods1.FirstName = names[1];
                methods1.Sex = names[2];
                methods1.Appointment = names[3];
                methods1.Date = names[4];
                methods1.Salary = int.Parse(names[5]);
                people.Add(methods1);
            }
            Console.WriteLine("Чтение данных прошло успешно.");
        }

        //Переопределения метода ToString
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", LastName, FirstName, Sex, Appointment, Date, Salary);
        }

        //Поиск
        public void SearchInfo(List<Methods> people)
        {
            Console.WriteLine("Введите должность: ");
            string App = Console.ReadLine();
            var result = people.FindAll(x => (x.Appointment.Contains(App)));
            foreach (var res in result)
            {
                Console.WriteLine(string.Format("{0} {1}", res.LastName, res.FirstName));
            }            
        }

        //Подсчёт
        public void CountWorkers(List<Methods> people)
        {
            Console.WriteLine("Введите должность: ");
            string App = Console.ReadLine();
            var result = people.FindAll(x => (x.Appointment.Contains(App)));
            Console.WriteLine("{0} : {1}", App, result.Count);
        }

        //Является ли разработчиком
        public void IsDeveloper()
        {
            Methods methods = new Methods();
            Console.WriteLine("Является ли сотрудник разработчиком?");
            if (this.GetType().Name == "Developer")
            {
                Console.WriteLine(true);
            }
            else
            {
                Console.WriteLine(false);
            }
        }
        
        //Удаление сотрудника
        public void RemovePerson()
        {
            Console.WriteLine("Введите номер сотрудника: ");
            int number = int.Parse(Console.ReadLine());
            number -= 1;
            people.RemoveAt(number);
            Console.WriteLine("Сотрудник удалён");
        }
    }
}
