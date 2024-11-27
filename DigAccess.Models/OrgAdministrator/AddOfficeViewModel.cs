using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.OrgAdministrator
{
    public class AddOfficeViewModel
    {
        public string Id { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Organisation { get; set; } = null!;

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
    }
}
