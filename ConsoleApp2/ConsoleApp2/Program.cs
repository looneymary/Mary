using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            double sum1 = 0;
            for (int x = -1; x < 5; x++)
            {
                sum1 += Math.Pow(x+5, 1/3f)+2.0f*x*(x/13.0f);
            }
            Console.Write(sum1);

            double sum2 = 0;
            for (int x = 1; x < 8; x++)
            {
                sum2 += Math.Sin(x) + Math.Exp(x);
            }
            Console.Write(sum2);

            ArrayList arr = new ArrayList();
            for (int x = -1; x < 4; x++)
            {
                double y = 2 * x + 5;
                if (y == 3)
                {
                    y = y * y;
                } 
                if (y == 13)
                {
                    y = Math.Sin(y);
                }
                arr.Add(y);
            }
            foreach ( double v in arr)
            Console.Write(v);
        }
    }
}
