using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazlab3proje
{
    internal class Table
    {
        public bool isEmpty = true;
        public static int no = 1;
        public string name = "Masa";
        public Table()
        {
            name= name + no;
            no++;
        }

    }
}
