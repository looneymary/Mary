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
        public int person;
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

            FullInfo = this.ToString();            
        }

        //Вывести всех
        public void ShowAllList()
        {
            int i = 1;
            foreach (var person in base.fullInfoArr)
            {
                Console.WriteLine(string.Format("| {0, 2} {1}", i, person));
                i++;
            }
        }

        //Показать одного сотрудника
        public virtual void ShowOnePerson( )
        {            
            Console.WriteLine("Введите порядковый номер:");
            person = int.Parse(Console.ReadLine());
            Console.WriteLine(fullInfoArr[person - 1]);
        }

        //Запись в файл
        public void WriteToFile()
        {
            string textToWriteInAFile = "";
            foreach (var a in fullInfoArr)
            {
                textToWriteInAFile += a.ToString() + "\r\n";
            }
            File.WriteAllText(FilePath, textToWriteInAFile);            
        }

        //Чтение из файла
        public virtual void ReadFromFile()
        {
            foreach (string line in File.ReadLines(FilePath))
            {
                fullInfoArr.Add(line);
            }
        }

        //Переопределения метода ToString
        public override string ToString()
        {
            return String.Format("  {0, 10}   {1, 10}   {2, 3}   {3, 10}   {4, 29}   {5, 10}  ", LastName, FirstName, Sex, Appointment, Date, Salary);
        }

        //Поиск НЕ РЕАЛИЗОВАН
        public void SearchInfo()
        {
            Console.WriteLine("Введите должность ");
            string App = Console.ReadLine();
            Console.WriteLine();
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
            int num = int.Parse(Console.ReadLine());
            num -= 1;
            fullInfoArr.RemoveAt(num);
            Console.WriteLine("Сотрудник удалён");
        }
    }
}
