using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magaz.HelperClass
{
    public class stringGen
    {
        public static string FIO()
        {
            string fio = string.Empty;
            for (int i = 1040; i < 1104; i++)
            {
                fio += (char)i;
            }
            return fio + "-";
        }
    }
}
