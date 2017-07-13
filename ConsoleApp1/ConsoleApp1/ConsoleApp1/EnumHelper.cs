﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class EnumHelper
    {        
        //Найти в строке перечисления
        public static T ReturnEnumFromString<T>(String strWithEnum)
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
                catch(ArgumentException)
                {
                }                
            }
            return default(T);
        }
    }
}
