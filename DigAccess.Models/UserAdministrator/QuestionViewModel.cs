using DigAccess.Common;
using System.ComponentModel.DataAnnotations;

namespace DigAccess.Models.UserAdministrator
{
    public class QuestionViewModel
    {
        [Required(ErrorMessage = QuestionConstants.RequiredNameError)]
        [MinLength(QuestionConstants.MinTitleLength, ErrorMessage = QuestionConstants.MinNameLengthError)]
        [MaxLength(QuestionConstants.MaxTitleLength, ErrorMessage = QuestionConstants.MaxNameLengthError)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = QuestionConstants.RequiredDescriptionError)]
        [MinLength(QuestionConstants.MinDescriptionLength, ErrorMessage = QuestionConstants.MinDescriptionLengthError)]
        [MaxLength(QuestionConstants.MaxDescriptionLength, ErrorMessage = QuestionConstants.MaxDescriptionLengthError)]
        public string Description { get; set; } = null!;

    }
}
