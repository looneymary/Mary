using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ValidEnum
    {
        /// <summary>
        /// Find and count enums from string
        /// </summary>
        /// <typeparam name="T">The type of the enum</typeparam>
        /// <param name="strWithEnum">String what probably contains enums</param>
        /// <returns>Number of enums</returns>
        public int ValidEnums<T>(String strWithEnum)
        {
            int countEnum = 0;
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("There is no enums here.");
            }

            string[] values = strWithEnum.Split(' ');
            foreach (var str in values)
            {
                try
                {
                    var enumValue = (T)Enum.Parse(typeof(T), str);
                    if (Enum.IsDefined(typeof(T), enumValue))
                    {
                        countEnum++;
                        return countEnum;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect value was entered.");
                        return countEnum;
                    }
                }
                catch (ArgumentException)
                {
                }
            }
            return countEnum;
        }

        /// <summary>
        /// Find and return enums from string
        /// </summary>
        /// <typeparam name="T">The type of the enum</typeparam>
        /// <param name="strWithEnum">String what probably contains enums</param>
        /// <returns>Enum value</returns>
        public T ReturnEnumFromString<T>(String strWithEnum)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("There is no enums here.");
            }

            string[] values = strWithEnum.Split(' ');
            foreach (var str in values)
            {
                try
                {
                    var enumValue = (T)Enum.Parse(typeof(T), str);
                    if (Enum.IsDefined(typeof(T), enumValue))
                    {
                        Console.WriteLine(enumValue);
                        return enumValue;
                    }
                    else
                    {
                        Console.WriteLine("The string doesn't contain enums.");
                        return default(T);
                    }
                }
                catch (ArgumentException)
                {
                }
            }
            return default(T);
        }
    }
}
