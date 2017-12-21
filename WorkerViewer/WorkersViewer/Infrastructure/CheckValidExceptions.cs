using System;
using System.Text.RegularExpressions;
using WorkersViewer.Properties;

namespace WorkerViewer.Infrastructure
{
    public class CheckValidExceptions : Exception
    {
        public enum NameOfElement { FirstName = 0, LastName = 1, Sex = 2, Appointment = 3, Date = 4, Salary = 5, DeveloperLanguage = 6, Experience = 7, Level = 8, YearsInService = 9 }

        public int ValidResult { get; set; }
        public string ExMessage { get; set; }
        public CheckValidExceptions() { }
        public CheckValidExceptions(string message) : base(message) { }

        /// <summary>
        /// Valid values and count exceptions
        /// </summary>
        /// <param name="parametres">The array of properties of workers</param>
        /// <returns>Count of found exceptions</returns>
        public int CheckExceptions(params string[] parametres)
        {
            ValidResult = 0;
            ExMessage = "";
            try
            {
                for (int i = 0; i < parametres.Length; i++)
                {
                    if (i == 4)
                    {
                        DateCheck(parametres[i], EnumToRightLanguage((NameOfElement)i));
                    }
                    else if (i == 5)
                    {
                        IntVal(int.Parse(parametres[i]), EnumToRightLanguage((NameOfElement)i));
                    }
                    else if (parametres.Length == 7 && i == 6)
                    {
                        IntVal(int.Parse(parametres[i]), EnumToRightLanguage((NameOfElement)i));
                    }
                    else
                    {
                        StringCheck(parametres[i], EnumToRightLanguage((NameOfElement)i));
                    }
                }
            }
            catch (CheckValidExceptions ex)
            {
                ValidResult++;
            }
            finally
            {
                if (ValidResult > 0)
                {
                    ExMessage += "\n" + Resources.WorkerNotAdd;
                }
            }
            return ValidResult;
        }

        /// <summary>
        /// Valid xml-elements and count exceptions
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
                Console.WriteLine(ex.ExMessage);
                ValidResult++;
            }
            catch
            {
                ValidResult++;
            }
            finally
            {
                if (ValidResult > 0)
                {
                   ExMessage = Resources.WorkerNotUpdate;
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
                string message = Resources.StrindExcBegin + "\"" + name +  "\" " + Resources.EmptyStrindExcEnd;
                ExMessage = message;
                throw new CheckValidExceptions(message);                
            }

            Regex regForText = new Regex(@"[А-Яа-яA-Za-z-]");
            MatchCollection mc = regForText.Matches(var);
            if (mc.Count == 0)
            {
                string message = Resources.StrindExcBegin + "\"" + name + "\" " + Resources.InvalidStringExcMiddle + Resources.InvalidStringExcEnd + var;
                ExMessage = message;
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
                string message = Resources.StrindExcBegin + "\"" + name + "\" " + Resources.DateExc;
                ExMessage = message;
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
            if (intVal <= 0 || intVal > 10000000)
            {
                string message = Resources.StrindExcBegin + "\"" + name + "\" " + Resources.IntLessThenZeroOrTooBig;
                ExMessage = message;
                throw new CheckValidExceptions(message);
            }
            if(name == "YearsInService" && intVal > 70)
            {
                string message = Resources.ExcForYearsInOffice + name + "\".";
                ExMessage = message;
                throw new CheckValidExceptions(message);
            } 
        }

        /// <summary>
        /// Return name of eloement on right language
        /// </summary>
        /// <param name="name">enum with element's name</param>
        /// <returns>name of element</returns>
        public string EnumToRightLanguage(NameOfElement name)
        {
            switch (name)
            {
                case NameOfElement.FirstName:
                    return Resources.FirstName;
                case NameOfElement.LastName:
                    return Resources.LastName;
                case NameOfElement.Sex:
                    return Resources.Sex;
                case NameOfElement.Appointment:
                    return Resources.Appointment;
                case NameOfElement.Date:
                    return Resources.Date;
                case NameOfElement.Salary:
                    return Resources.Salary;
                case NameOfElement.DeveloperLanguage:
                    return Resources.DevLang;
                case NameOfElement.Experience:
                    return Resources.Experience;
                case NameOfElement.Level:
                    return Resources.Level;
                case NameOfElement.YearsInService:
                    return Resources.YearsInService;
            }
            return null;
        }
    }
}
