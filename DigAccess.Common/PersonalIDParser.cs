﻿using System;
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
            int lastNumber = int.Parse(personalID[9].ToString());

            if (lastNumber % 2 == 0)
            {
                return "Жена";
            }
            else
            {
                return "Мъж";
            }
        }
        // Извличане на рожденната дата от ЕГН
        public static DateTime BirthdateExtract(string personalID)
        {
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

            return new DateTime(year, month, day);
        }
    }
}
