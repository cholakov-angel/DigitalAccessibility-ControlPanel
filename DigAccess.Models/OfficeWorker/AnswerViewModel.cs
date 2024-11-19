using DigAccess.Common;
using System.ComponentModel.DataAnnotations;

namespace DigAccess.Models.OfficeWorker
{
    public class AnswerViewModel
    {
        public string? Id { get; set; }

        [Required]
        public string QuestionId { get; set; } = null!;

        [Required(ErrorMessage = AnswerConstants.RequiredNameError)]
        [MaxLength(QuestionConstants.MaxDescriptionLength, ErrorMessage = AnswerConstants.MaxNameLengthError)]
        [MinLength(QuestionConstants.MinDescriptionLength, ErrorMessage = AnswerConstants.MinNameLengthError)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = AnswerConstants.RequiredDescriptionError)]
        [MaxLength(QuestionConstants.MaxDescriptionLength, ErrorMessage = AnswerConstants.MaxDescriptionLengthError)]
        [MinLength(QuestionConstants.MinDescriptionLength, ErrorMessage = AnswerConstants.MinDescriptionLengthError)]
        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}
