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
        public enum NameOfElement { FirstName = 0, LastName = 1, Sex = 2, Appointment = 3, Date = 4, Salary = 5, DeveloperLanguage = 6, Experience = 7, Level = 8, YearsInService = 9 }

        public int ValidResult { get; set; }
        public CheckValidExceptions() { }
        public CheckValidExceptions(string message) : base(message) { }

        /// <summary>
        /// Valid values and count exceptions
        /// </summary>
        /// <param name="parametres">The array of properties of workers</param>
        /// <returns>Count of found exceptions</returns>
        public int CheckExceptions(params string[] parametres)
        {
            Console.WriteLine("Validation of data.");
            ValidResult = 0;
            try
            {
                for (int i = 0; i < parametres.Length; i++)
                {

                    if (i == 4)
                    {
                        DateCheck(parametres[i], ((NameOfElement)i).ToString());
                    }
                    else if (i == 5)
                    {
                        IntVal(int.Parse(parametres[i]), ((NameOfElement)i).ToString());
                    }
                    else if (parametres.Length == 7 && i == 6)
                    {
                        IntVal(int.Parse(parametres[i]), ((NameOfElement)9).ToString());
                    }
                    else if (parametres.Length == 9 && i == 7)
                    {
                        IntVal(int.Parse(parametres[i]), ((NameOfElement)i).ToString());
                    }
                    else
                    {
                        StringCheck(parametres[i], ((NameOfElement)i).ToString());
                    }
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
        /// 
        /// </summary>
        /// <param name="value">New value</param>
        /// <param name="numOfElement">Number of updating element in enum NameOfElement</param>
        /// <returns>Count of found exceptions</returns>
        public int CheckXmlExeptions(string value, int numOfElement)
        {
            ValidResult = 0;
            numOfElement -= 1;
            try
            {
                switch ((NameOfElement)numOfElement)
                {
                    case NameOfElement.FirstName:
                    case NameOfElement.LastName:
                    case NameOfElement.Sex:
                    case NameOfElement.Appointment:
                        StringCheck(value, ((NameOfElement)numOfElement).ToString());                        
                        return ValidResult;
                    case NameOfElement.Date:
                        DateCheck(value, ((NameOfElement)numOfElement).ToString());
                        return ValidResult;
                    case NameOfElement.Salary:
                        IntVal(int.Parse(value), ((NameOfElement)numOfElement).ToString());
                        return ValidResult;
                    case NameOfElement.DeveloperLanguage:
                        StringCheck(value, ((NameOfElement)numOfElement).ToString());
                        return ValidResult;
                    case NameOfElement.Experience:
                        IntVal(int.Parse(value), ((NameOfElement)numOfElement).ToString());
                        return ValidResult;
                    case NameOfElement.Level:
                        StringCheck(value, ((NameOfElement)numOfElement).ToString());
                        return ValidResult;
                    case NameOfElement.YearsInService:
                        IntVal(int.Parse(value), ((NameOfElement)numOfElement).ToString());
                        return ValidResult;
                }
            }
            catch (CheckValidExceptions ex)
            {
                Console.WriteLine(ex.Message);
                ValidResult++;
            }
            catch
            {
                ValidResult++;
            }
            finally
            {
                if (ValidResult == 0)
                {
                    Console.WriteLine("Validation was successful.");
                }
                else
                {
                    Console.WriteLine("The employee wasn't update. Repeat input.");
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
