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

        /// <summary>
        /// Valid values and count exceptions
        /// </summary>
        /// <param name="parametres">The array of properties of workers</param>
        /// <returns>Number of found exceptions</returns>
        public int CheckExceptions(params string[] parametres)
        {
            Console.WriteLine("Validation of data.");
            ValidResult = 0;
            try
            {  
                StringCheck(parametres[0].ToString(), "First name");
                StringCheck(parametres[1].ToString(), "Last name");
                StringCheck(parametres[2].ToString(), "Sex");
                StringCheck(parametres[3].ToString(), "Appointment");
                DateCheck(parametres[4].ToString(), "Date of taking office");
                IntVal(int.Parse(parametres[5].ToString()), "Salary");
                if (parametres.Length == 7)
                {
                    IntVal(int.Parse(parametres[6].ToString()), "How many years in service");
                }
                if(parametres.Length == 9)
                {
                    StringCheck(parametres[6].ToString(), "Development language");
                    IntVal(int.Parse(parametres[7].ToString()), "Experience");
                    StringCheck(parametres[8].ToString(), "Level");
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
                    Console.WriteLine("Validation was successful.");
                }
                else
                {
                    Console.WriteLine("The employee wasn't addede. Repeat input.");
                }
            }
            return ValidResult;
        }

        /// <summary>
        /// Validation of string values
        /// </summary>
        /// <param name="var">The value what need to valid</param>
        /// <param name="name">The name of field</param>
        public void StringCheck(string var, string name)
        {
            if (String.IsNullOrEmpty(var))
            {
               string message = "The field \""+ name +"\" wasn't filled. Please, correct it.";
               throw new CheckValidExceptions(message);
            }

            Regex regForText = new Regex(@"[А-Яа-яA-Za-z-]");
            MatchCollection mc = regForText.Matches(var);
            if(mc.Count == 0)
            {
                string message = "The field \"" + name + "\" conteins invalid characters. " + var + " was introdused.";
                throw new CheckValidExceptions(message);
            }
        }

        /// <summary>
        /// Validation of date values
        /// </summary>
        /// <param name="var">The value what need to valid</param>
        /// <param name="name">The name of field</param>
        public void DateCheck(string var, string name)
        {
            Regex reg = new Regex(@"(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.(19|20)\d\d");
            MatchCollection mc = reg.Matches(var);
            if (mc.Count == 0)
            {
                string message = "The field \"" + name + "\" not filled in correctly. Date format must be \"dd.mm.yyyy\".";
                throw new CheckValidExceptions(message);
            }
        }

        /// <summary>
        /// Validation of int values
        /// </summary>
        /// <param name="intVal">The value what need to valid</param>
        /// <param name="name">The name of field</param>
        public void IntVal(int intVal, string name)
        {
            if (intVal <= 0)
            {
                string message = "The field \"" + name + "\" not filled in correctly. The value must be greater, than 0.";
                throw new CheckValidExceptions(message);
            }
        }
    }
}
