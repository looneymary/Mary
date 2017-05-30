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
                        param.AddInfo();

                        break;
                    //Вывести список всех сотрудников
                    //Не придумала как пронумеровать этот список
                    case 2:
                        //Вынести в функцию
                        param.ShowAllList();
                        break;
                    //Вывести информацию об одном сотруднике
                    case 3:
                        //Вынести в функцию
                        param.ShowOnePerson();
                        break;
                    //Запись списка в файл
                    case 4:
                        //I eta v funkciu
                        param.WriteToFile();
                        break;
                    //Чтение данных из файла
                    //Не нравится, что дублируются данные при выборе чтения из файла и добавления в массив, 
                    //но как это фильтровать тоже что-то не придумала
                    case 5:
                        //4itat' iz faila
                        param.ReadFromFile();
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
