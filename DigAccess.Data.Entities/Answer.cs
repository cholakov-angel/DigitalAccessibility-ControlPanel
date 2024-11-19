using DigAccess.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Entities
{
    public class Answer
    {
        public Answer()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; } = null!;

        [Required]
        [MaxLength(AnswerConstants.MaxTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(AnswerConstants.MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsReviewed { get; set; }
    }
}
