using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Common
{
    public static class UserConstants
    {
        // Грешки
        public const string EmailRequired = "Имейлът е задължителен!";
        public const string EmailFormatError = "Въведният имейл не е във валиден формат!";

        public const string PasswordRequired = "Паролата е задължителна!";
        public const string PasswordLength = "Дължината на паролата трябва да е между 6 и 100 символа!";
        public const string PasswordNotMatch = "Паролите не съвпадат!";

        public const string NameRequired = "Името е задължително!";
        public const string MiddleNameRequired = "Бащиното име е задължително!";
        public const string LastNameRequired = "Фамилията е задължителна!";

        public const string PersonalIDRequired = "ЕГН е задължително!";

        public const string PhoneRequired = "Телефонният номер е задължителен!";
        public const string PhoneFormatError = "Телефонът не е във валиден формат!";
    } // UserConstants
}
