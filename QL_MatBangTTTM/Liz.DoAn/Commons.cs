using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liz.DoAn
{
   public class Commons
    {
        public static bool IsValidDate(string inputValue, string format)
        {
            try
            {
                var date = DateTime.ParseExact(inputValue.ToString(), format, CultureInfo.CurrentCulture);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static DateTime ConvertStringToDate(string date)
        {         
            DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            return dateTime;
        }
    }
}
