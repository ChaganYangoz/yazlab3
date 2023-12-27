using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazlab3proje
{
    internal class Chef
    {
        public static int no = 1;
        public string name = "Asci";
        public Chef()
        {
            name += no;
            no++;
        }

    }
}
