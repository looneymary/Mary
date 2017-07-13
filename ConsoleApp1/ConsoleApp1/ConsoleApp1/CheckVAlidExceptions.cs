using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class CheckValidExceptions : Exception
    {
        public int ValidResult { get; set; }
        public CheckValidExceptions() { }
        public CheckValidExceptions(string message) : base(message) { }

        public enum NameOfStrings { FirstName, LastName, Sex, Appointment}

        public int CheckExceptions(params string[] parametres)
        {
            Console.WriteLine("Валидация данных.");
            ValidResult = 0;
            int i = 0;
            try
            {   foreach (string param in parametres)
                {
                    if (i == 4)
                    {
                        DateCheck(param, "Дата вступления в должность");
                    }
                    else if (i == 5)
                    {
                        IntVal(int.Parse(param), "Оклад");
                    }
                    else
                    {
                        StringCheck(param, i);
                    }
                    i++;
                } 
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

        public void StringCheck(string var, int name)
        {
            if (String.IsNullOrEmpty(var))
            {
               string message = "Поле \""+ (NameOfStrings)name +"\" не было заполнено. Пожалуйста исправьте.";
               throw new CheckValidExceptions(message);
            }

            Regex regForText = new Regex(@"[А-Яа-яA-Za-z-]");
            MatchCollection mc = regForText.Matches(var);
            if(mc.Count == 0)
            {
                string message = "Поле \"" + (NameOfStrings)name + "\" содержит недопустимые символы. Было введено: " + var;
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
