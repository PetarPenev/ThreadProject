using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalThreads
{
    public static class Helper
    {
        public static string Print(this List<string> list)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < list.Count; i++)
			{
                str.Append(list[i]);
                str.Append(" ");
			}

            return str.ToString();
        }
    }
}
