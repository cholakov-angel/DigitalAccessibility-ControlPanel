using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Keys
{
    public static class BlindUserKey
    {
        public static async Task<string> GenerateKey(string name, string personalID, Random random)
        {
            // Разбъркване на символите в ЕГН и пълното име
            personalID = RandomizeString(personalID, random);
            name = RandomizeString(name, random);

            string temp = default;

            // Редуване на символ от ЕГН и името, в поредност определена
            // от четността на случайно генерирано число
            var startNumber = random.Next();
            for (int i = 0; i < personalID.Length; i++)
            {
                if (startNumber % 2 == 0)
                {
                    temp += name[i].ToString();
                }
                else
                {
                    temp += personalID[i];
                }

                startNumber++;
            }

            // Разбъркване на символите в получения временен резултат
            temp = RandomizeString(temp, random);

            string key = default;

            // Представяне на всеки един от получените символи в шестнайсетичен вид
            for (int i = 0; i < temp.Length; i++)
            {
                int result = temp[i];
                var hexStyle = result.ToString("X");
                key += hexStyle;
            }

            // Разбъркване на символите в получения клуч
            key = RandomizeString(key, random);

            return key;
        } // GenerateMasterkey

        private static string RandomizeString(string str, Random random)
        {
            List<int> usedIndexes = new List<int>();
            string result = "";

            while (result.Length != str.Length)
            {
                int index = random.Next(0, str.Length);
                while (usedIndexes.Contains(index))
                {
                    index = random.Next(0, str.Length);
                }

                result += str[index];
                usedIndexes.Add(index);
            }
            return result;
        } // RandomizeString
    }
}
