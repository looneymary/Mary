using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class CheckValidExceptions : Exception
    {
        public int ValidResult { get; set; }
        public CheckValidExceptions() { }
        public CheckValidExceptions(string message) : base(message) { }

        public int CheckExceptions(string FirstName, string LastName, string Sex, string Appointment, 
                                   string Date, int Salary)
        {
            CheckValidExceptions exc = new CheckValidExceptions();

            Console.WriteLine("Валидация данных.");
            ValidResult = 0;
            try
            {                
                StringCheck(FirstName, "Имя");
                StringCheck(LastName, "Фамилия");
                StringCheck(Sex, "Пол");
                StringCheck(Appointment, "Должность");
                DateCheck(Date, "Дата вступления в должность");
                IntVal(Salary, "Оклад");
            }
            catch(CheckValidExceptions ex)
            {
                Console.WriteLine(ex.Message);
                ValidResult++;
            }            
            finally
            {
                if(ValidResult == 0)
                {
                    Console.WriteLine("Валидация прошла успешно.");
                }
                else
                {
                    Console.WriteLine("Сотрудник не был добавлен. Повторите ввод.");
                }
            }
            return ValidResult;
        }

        public void StringCheck(string var, string name)
        {
            if (String.IsNullOrEmpty(var))
            {
               string message = "Поле \""+name+"\" не было заполнено. Пожалуйста исправьте.";
               throw new CheckValidExceptions(message);
            }

            Regex reg = new Regex(@"[А-Яа-яA-Za-z-]");
            MatchCollection mc = reg.Matches(var);
            if(mc.Count == 0)
            {
                string message = "Поле \"" + name + "\" содержит недопустимые символы. Было введено: " + var;
                throw new CheckValidExceptions(message);
            }
        }

        public void DateCheck(string var, string name)
        {
            Regex reg = new Regex(@"(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.(19|20)\d\d");
            MatchCollection mc = reg.Matches(var);
            if (mc.Count == 0)
            {
                string message = "Поле \"" + name + "\" было заполнено не верно. Формат даты должен быть \"чч.мм.гггг\".";
                throw new CheckValidExceptions(message);
            }
        }

        public void IntVal(int intVal, string name)
        {
            if (intVal <= 0)
            {
                string message = "Поле \"" + name + "\" было заполнено не верно. Значение должно быть большо 0.";
                throw new CheckValidExceptions(message);
            }
        }
    }
}
