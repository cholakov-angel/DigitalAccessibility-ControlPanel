using System.ComponentModel.DataAnnotations;

namespace DigAccess.Models.OrgAdministrator
{
    public class OrganisationViewModel
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public string National_Phone { get; set; } = null!;
    }
}
