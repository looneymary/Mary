using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;
            Developer developer = new Developer();
            OfficeWorker officeWorker = new OfficeWorker();
            Methods methods = new Methods();

            do
            {
                Console.Write("Меню:\n1. Ввести данные о сотруднике \n2. Вывести информацию о всех сотрудниках \n" +
                                    "3. Вывести информацию об одном сотруднике \n4. Запись информации в файл\n5. Чтение из файла \n6. Поиск по должности \n7. Выйти из программы\n\n");
                i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    //Добавить информацию о сотруднике
                    case 1:

                        int type;

                        Console.WriteLine("Информация о сотруднике: \n");

                        Console.WriteLine("Укажите тип:\n1). Разработчик \n2). Работник офиса \n ");
                        type = int.Parse(Console.ReadLine());
                        if (type == 1)
                        {
                            developer.AddInfo();
                            //Добавить пользователя в коллекцию
                            methods.fullInfoArr.Add(developer);
                        }
                        else if (type == 2) 
                        {
                            officeWorker.AddInfo();
                            //Добавить пользователя в коллекцию
                            methods.fullInfoArr.Add(officeWorker);
                        }
                        else
                        {
                            Console.WriteLine("Введены неверные данные");
                        }
                        
                        break;
                    //Вывести список всех сотрудников
                    case 2:
                        string shapka = string.Format("| {0, 2} | {1, 10} | {2, 10} | {3, 3} | {4, 10} | {5, 29} | {6, 10} |", "№", "Фамилия", "Имя", "Пол", "Должность", "Дата вступления в должность", "Оклад");
                        Console.WriteLine(shapka);
                        methods.ShowAllList();
                        break;
                    //Вывести информацию об одном сотруднике
                    case 3:
                        methods.ShowOnePerson();
                        break;
                    //Запись списка в файл
                    case 4:
                        methods.WriteToFile();
                        Console.WriteLine("Запись данных прошла успешно.");
                        break;
                    //Чтение данных из файла
                    case 5:
                        methods.ReadFromFile();
                        Console.WriteLine("Чтение данных прошло успешно.");
                        break;
                    //Поиск по должности
                    case 6:
                        //methods.SearchInfo();
                        //methods.IsDeveloper();
                        methods.RemovePerson();
                        break;
                    case 7:
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
            while (i != 7);
        }
    }
}
