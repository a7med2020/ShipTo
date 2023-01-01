using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.Utilities
{
    public static class StringFunctions
    {
        public static string ArabicNumToEnglish(string input)
        {
            String[] map = { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };

            for (int i = 0; i <= 9; i++)
            {
                input = input.Replace(map[i], i.ToString());
            }

            return input;
        }
    }
}
