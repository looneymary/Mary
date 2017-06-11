using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class OfficeWorker : Methods
    {
        public OfficeWorker() : base()
        {            
        }

        public override void ShowOnePerson()
        {
            base.ShowOnePerson();
            Console.WriteLine(str);
        }
    }
}
