using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazlab3
{
    public class Customer
    {
        public static int no =1;
        public bool IsPriority { get; }
        public bool atDesk=false;
        public bool canOrder = true;
        public Thread thread;
        public string name="Musteri";
        public string TableName = "";

        public Customer(bool isPriority)
        {
            IsPriority = isPriority;
            if(isPriority)
            {
                name = "Oncelikli" + name + no;
            }
            else
            {
                name += no;
            }
            no++;
            thread = new Thread(waitInline);
           // thread.Start();
        }

        private void waitInline()
        {
            Thread.Sleep(5000);
            if(!atDesk)
            {
                // thread.Abort();
                Console.WriteLine("Musteri hala bekliyor");
            }
        }
       
    }
}
