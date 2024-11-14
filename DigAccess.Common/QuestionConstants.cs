namespace DigAccess.Common
{
    public class QuestionConstants
    {
        // Дължина
        public const int MaxTitleLength = 100;
        public const int MinTitleLength = 3;
        public const int MaxDescriptionLength = 8000;
        public const int MinDescriptionLength = 3;

        // Грешки
        public const string MaxNameLengthError = "Не може името да превишава 100 символа!";
        public const string MinNameLengthError = "Не може името да бъде по-малко от 3 символа!";
        public const string RequiredNameError = "Името е задължително!";

        public const string MaxDescriptionLengthError = "Не може описанието да превишава 8000 символа!";
        public const string MinDescriptionLengthError = "Не може описанието да бъде по-малко от 3 символа!";
        public const string RequiredDescriptionError = "Описанието е задължително!";
    } // QuestionConstants
}
