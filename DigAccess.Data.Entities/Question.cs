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
    public class Question
    {
        public Question()
        {
            this.Id = new Guid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(QuestionConstants.MaxTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(QuestionConstants.MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsAnswered { get; set; }
    }
}
