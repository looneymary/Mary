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
            elementArr.Add(firstName);

            Console.WriteLine("Фамилия:");
            string lastName = Console.ReadLine();
            elementArr.Add(lastName);

            Console.WriteLine("Пол (м/ж):");
            string sex = Console.ReadLine();
            elementArr.Add(sex);

            Console.WriteLine("Должность:");
            string appointment = Console.ReadLine();
            elementArr.Add(appointment);

            Console.WriteLine("\nРабочая информация: \n");

            Console.WriteLine("Дата вступления в должность:");
            string date = Console.ReadLine();
            elementArr.Add(date);

            Console.WriteLine("Оклад:");
            string val = Console.ReadLine();
            int salary = int.Parse(val);
            elementArr.Add(salary);
        }
        public void showAllList()
        {
            int arrCount = elementArr.Count;
            int num = 1;
            for (int j = 0; j<arrCount; j += 6)
            {
                //Краткая информация
                string text = num + ". " + elementArr[j + 1] + " " + elementArr[j] + ", "
                                         + elementArr[j + 2] + ", " + elementArr[j + 3];
                //Полная инфа
                string fullInfo = num + ". " + elementArr[j + 1] + " " + elementArr[j] + ", "
                                         + elementArr[j + 2] + ", " + elementArr[j + 3] + ", "
                                         + elementArr[j + 4] + ", " + elementArr[j + 5];
                //Заполнение коллекции
                fullInfoArr.Add(fullInfo);
                num++;
                Console.Write(text);
                Console.WriteLine("\n");
            }
        }
        public void writeToFile()
        {
            string textToWriteInAFile = "";
            foreach (var a in fullInfoArr)
            {
                textToWriteInAFile += " " + (string)a + ";\n";
            }
            File.WriteAllText(filePath, textToWriteInAFile);
        }
        public void readFromFile()
        {
            string readText = File.ReadAllText(filePath);
            Console.WriteLine(readText);
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
                                    "3. Запись информации в файл \n4. Чтение из файла \n5. Выйти из программы\n\n");
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
                    //Запись списка в файл
                    case 3:
                        //I eta v funkciu
                        param.writeToFile();
                        break;
                    //Чтение данных из файла
                    case 4:
                        //4itat' iz faila
                        param.readFromFile();
                        break;
                    case 5:
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
            while (i != 5);
        }
    }
}
