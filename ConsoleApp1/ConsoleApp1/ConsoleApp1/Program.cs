﻿using System;
using ConsoleApp1.Models;
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
            Repository repository = new Repository();
            WorkWithFile file = new WorkWithFile();
            Viewer viewer = new Viewer();
            do
            {
                Console.Write("Меню:\n1. Ввести данные о сотруднике \n2. Вывести информацию о всех сотрудниках \n" +
                                    "3. Вывести информацию об одном сотруднике \n4. Запись информации в файл\n5. Чтение из файла \n" +
                                    "6. Поиск по должности \n7. Подсчёт по должности \n8. Удалить сотрудника \n9. Выйти из программы\n\n");
                i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    // Добавить информацию о сотруднике.
                    case 1:                        
                        Developer developer = new Developer();
                        OfficeWorker officeWorker = new OfficeWorker();
                        
                        int type;

                        Console.WriteLine("Информация о сотруднике: \n");

                        Console.WriteLine("Укажите тип:\n1). Разработчик \n2). Работник офиса \n ");
                        type = int.Parse(Console.ReadLine());
                        if (type == 1)
                        {
                            developer.AddInfo();
                            // Добавить пользователя в коллекцию.
                            repository.people.Add(developer);
                        }
                        else if (type == 2) 
                        {
                            officeWorker.AddInfo();
                            // Добавить пользователя в коллекцию.
                            repository.people.Add(officeWorker);
                        }
                        else
                        {
                            Console.WriteLine("Введены неверные данные");
                        }                        
                        break;
                    // Вывести список всех сотрудников.
                    case 2:
                        viewer.ShowAllList(repository.people);
                        break;
                    // Вывести информацию об одном сотруднике.
                    case 3:
                        repository.ShowOnePerson(repository.people);
                        viewer.ShowAllList(repository.onePerson);
                        break;
                    // Запись списка в файл.
                    case 4:
                        file.WriteToFile(repository.people);
                        break;
                    // Чтение данных из файла.
                    case 5:                        
                        file.ReadFromFile(repository.people);
                        break;
                    // Поиск по должности.
                    case 6:
                        repository.SearchInfo(repository.people);                         
                        break;
                    // Подсчёт по должности.
                    case 7:
                        repository.CountWorkers(repository.people);
                        break;
                    // Удалить сотрудника.
                    case 8:
                        repository.RemovePerson();
                        break;
                    // Поиск по должности.
                    case 9:
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
            while (i != 9);
        }
    }
}