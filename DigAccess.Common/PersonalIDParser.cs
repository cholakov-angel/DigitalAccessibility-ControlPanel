using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Common
{
    public static class PersonalIDParser
    {
        // Извличане на пола от ЕГН
        public static string GenderExtract(string personalID)
        {
            int preLastNumber = int.Parse(personalID[8].ToString());

            if (preLastNumber % 2 != 0)
            {
                return "Жена";
            }
            else
            {
                return "Мъж";
            }
        } // GenderExtract

        // Извличане на рожденната дата от ЕГН
        public static DateTime BirthdateExtract(string personalID)
        {
            DateTime date;

            int year = int.Parse(string.Concat(personalID[0], personalID[1]));
            int month = int.Parse(string.Concat(personalID[2], personalID[3]));
            int day = int.Parse(string.Concat(personalID[4], personalID[5]));

            if (month > 40)
            {
                year += 2000;
                month -= 40;
            }
            else if (month > 20)
            {
                year += 1800;
                month -= 20;
            }
            else
            {
                year += 1900;
            }

            if (year > DateTime.Now.Year || month > 12 || month < 1 || day > 31 || day < 1)
            {
                return default;
            }
            return new DateTime(year, month, day);
        } // BirthdateExtract
    } // PersonalIDParser
}
