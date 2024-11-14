using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigAccess.Models.UserAdministrator.EmailSettings
{
    public class EmailAddViewModel
    {
        public string? Id { get; set; }

        [Required]
        public string BlindUserId { get; set; } = null!;

        [Required]
        public string AdministratorId { get; set; } = null!;


        [Required(ErrorMessage = "Полето е задължително!")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;

        public string EmailProvider { get; set; }

        public List<EmailProviderViewModel>? EmailProviders { get; set; }
    }
}
