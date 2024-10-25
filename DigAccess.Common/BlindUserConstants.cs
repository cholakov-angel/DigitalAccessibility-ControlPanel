using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Common
{
    public static class BlindUserConstants
    {
        // Length
        public const int MaxNameLength = 60;
        public const int MinNameLength = 3;
        public const int PersonalIDLength = 10;

        // Errors
        public const string MaxNameLengthError = "Не може името да превишава 60 символа!";
        public const string MinNameLengthError = "Не може името да бъде по-малко от 3 символа!";
        public const string RequiredNameError = "Името е задължително!";

        public const string MaxMiddleNameLengthError = "Не може бащиното име да превишава 60 символа!";
        public const string MinMiddleNameLengthError = "Не може бащиното име да бъде по-малко от 3 символа!";
        public const string RequiredMiddleNameError = "Бащиното име е задължително!";

        public const string MaxLastNameLengthError = "Не може фамилията да превишава 60 символа!";
        public const string MinLastNameLengthError = "Не може фамилията да бъде по-малко от 3 символа!";
        public const string RequiredLastNameError = "Фамилията е задължителна!";

        public const string PersonalIDLengthError = "Дължината на ЕГН трябва да е 10 цифри!";
        public const string PersonalIDError = "Невалидно ЕГН!";
        public const string RequiredPersonalIDError = "ЕГН е задължително!";

        public const string RequiredTELKIDError = "Номерът на ТЕЛК е задължителен!";



    }
}
