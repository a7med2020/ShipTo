using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public static string ConvertImageToBase64String(string imagePath)
        {
            string result = null;
            if (!string.IsNullOrEmpty(imagePath))
            {
                using (var b = new Bitmap(imagePath))
                {
                    using (var ms = new MemoryStream())
                    {
                        b.Save(ms, ImageFormat.Bmp);
                        result = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return result;
        }
    }
}
