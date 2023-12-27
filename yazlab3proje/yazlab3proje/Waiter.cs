using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yazlab3;

namespace yazlab3proje
{
    public class Waiter
    {
        public Thread thread;
        public Thread customerThread;
        public static int no = 1;
        public string name = "Garson";
        public bool isAvaliable = true;

        public Waiter() 
        {
            name += no;
            no++;
        }

        public void SetThread(Customer customer)
        {
             thread = new Thread(() => TakeOrder(customer));
        }

        public void TakeOrder(Customer customer)
        {
            Thread.Sleep(1000);
            Console.WriteLine(customer.name + "in siparisini " + name + " aldi");
        }
    }
}
