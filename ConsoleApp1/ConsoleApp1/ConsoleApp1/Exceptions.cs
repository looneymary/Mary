using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class MyExceptions : Exception
    {
        public int Result { get; set; }
        public MyExceptions() { }
        public MyExceptions(string message) : base(message) { }
        public MyExceptions(string message, Exception ex) : base(message) { }

        public int CheckExceptions(string a, string b, string c, string d, string f, int g)
        {
            Repository rep = new Repository();
            OfficeWorker office = new OfficeWorker();
            MyExceptions exc = new MyExceptions();

            Console.WriteLine("Валидация данных.");
            Result = 0;
            try
            {                
                exc.StringCheck(a, "Имя");
                exc.StringCheck(b, "Фамилия");
                exc.StringCheck(c, "Пол");
                exc.StringCheck(d, "Должность");
                exc.DateCheck(f, "Дата вступления в должность");
                exc.IntVal(g, "Оклад");
            }
            catch(MyExceptions ex)
            {
                Console.WriteLine(ex.Message);
                Result++;
            }
            finally
            {
                if(Result == 0)
                {
                    Console.WriteLine("Валидация прошла успешно.");
                }
                else
                {
                    Console.WriteLine("Сотрудник не был добавлен. Повторите ввод.");
                }
            }
            return Result;
        }

        public void StringCheck(string var, string name)
        {
            if (var == string.Empty || var == null)
            {
               string message = "Поле \""+name+"\" не было заполнено. Пожалуйста исправьте.";
               throw new MyExceptions(message);
            }

            Regex reg = new Regex(@"[А-Яа-я]");
            MatchCollection mc = reg.Matches(var);
            if(mc.Count == 0)
            {
                string message = "Поле \"" + name + "\" содержит недопустимые символы. Было введено: " + var;
                throw new MyExceptions(message);
            }
        }

        public void DateCheck(string var, string name)
        {
            Regex reg = new Regex(@"[0-9]+.[0-9]+.[0-9]+");
            MatchCollection mc = reg.Matches(var);
            if (mc.Count == 0)
            {
                string message = "Поле \"" + name + "\" было заполнено не верно. Формат даты должен быть \"чч.мм.гггг\".";
                throw new MyExceptions(message);
            }
        }

        public void IntVal(int intVal, string name)
        {
            int s1 = intVal;
            if (intVal <= 0)
            {
                string message = "Поле \"" + name + "\" было заполнено не верно. Значение должно быть большо 0.";
                throw new MyExceptions(message);
            }
        }
    }
}
