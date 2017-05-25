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
        private ArrayList elementArr;
        private ArrayList fullInfoArr;

        public Params()
        {
            //Путь к файлу
            filePath = @"D:\net\projects\looneyTypeOfCoding\ConsoleApp1\spisok.txt";

            //Коллекция отдельных элементов
            elementArr = new ArrayList();

            //Коллекция строк со всей инфой для записи в файл
            fullInfoArr = new ArrayList();

        }

        public void addInfo()
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

            string fullInfo = lastName + " " + firstName + ", " + sex + ", " + appointment + ", "
                                         + date + ", " + salary;
            //Добавить пользователя в коллекцию
            fullInfoArr.Add(fullInfo);
            }
        public void showAllList()
        {
            foreach (var person in fullInfoArr)
                {
                    Console.WriteLine(person);
                }
        }
        public void showOnePerson()
        {
            int person;
            Console.WriteLine("Введите порядковый номер:");
            person = int.Parse(Console.ReadLine());
            Console.WriteLine(fullInfoArr[person-1]);
        }
        public void writeToFile()
        {
            string textToWriteInAFile = "";
            foreach (var a in fullInfoArr)
            {
                textToWriteInAFile += " " + (string)a + ";\r\n";
            }
            File.WriteAllText(filePath, textToWriteInAFile);
        }
        public void readFromFile()
        {
            
            foreach (string line in File.ReadLines(filePath))
            {
                fullInfoArr.Add(line);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int i;
            Params param = new Params();            
            do
            {
                Console.Write("Меню:\n1. Ввести данные о сотруднике \n2. Вывести информацию о всех сотрудниках \n" +
                                    "3. Вывести информацию об одном сотруднике \n4. Запись информации в файл\n5. Чтение из файла \n6. Выйти из программы\n\n");
                i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    //Добавить информацию о сотруднике
                    case 1:
                        //Вынести в функцию
                        param.addInfo();

                        break;
                    //Вывести список всех сотрудников
                    case 2:
                        //Вынести в функцию
                        param.showAllList();
                        break;
                    //Вывести информацию об одном сотруднике
                    case 3:
                        //Вынести в функцию
                        param.showOnePerson();
                        break;
                    //Запись списка в файл
                    case 4:
                        //I eta v funkciu
                        param.writeToFile();
                        break;
                    //Чтение данных из файла
                    case 5:
                        //4itat' iz faila
                        param.readFromFile();
                        break;
                    case 6:
                        Console.WriteLine("Закрыть приложение");
                        break;
                    default:
                        Console.WriteLine("Такого элемента нет в списке меню");
                        break;
                }
                Console.Write("\n\n\t\t\tВернуться к главному меню...");
                Console.ReadLine();
                Console.Clear();
            }
            while (i != 6);
        }
    }
}
