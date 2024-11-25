using System.ComponentModel.DataAnnotations;

namespace DigAccess.Models.OrgAdministrator
{
    public class EditOfficeViewModel
    {
        public string Id { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [MinLength(10)]
        [MaxLength(10)]
        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public int StreetNumber { get; set; }

        public string? CityName { get; set; }

        public List<CityViewModel>? Cities { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        [Required]
        public string Street { get; set; } = null!;
    } // OfficeViewModel
}
