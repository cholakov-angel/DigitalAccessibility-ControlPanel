using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Common;

namespace DigAccess.Models.UserAdministrator.BlindUser
{
    public class BlindUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = BlindUserConstants.RequiredNameError)]
        [MinLength(BlindUserConstants.MinNameLength, ErrorMessage = BlindUserConstants.MinNameLengthError)]
        [MaxLength(BlindUserConstants.MaxNameLength, ErrorMessage = BlindUserConstants.MaxNameLengthError)]

        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = BlindUserConstants.RequiredMiddleNameError)]
        [MinLength(BlindUserConstants.MinNameLength, ErrorMessage = BlindUserConstants.MinMiddleNameLengthError)]
        [MaxLength(BlindUserConstants.MaxNameLength, ErrorMessage = BlindUserConstants.MaxMiddleNameLengthError)]
        public string MiddleName { get; set; } = null!;

        [Required(ErrorMessage = BlindUserConstants.RequiredLastNameError)]
        [MinLength(BlindUserConstants.MinNameLength, ErrorMessage = BlindUserConstants.MinLastNameLengthError)]
        [MaxLength(BlindUserConstants.MaxNameLength, ErrorMessage = BlindUserConstants.MaxLastNameLengthError)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = BlindUserConstants.RequiredPersonalIDError)]
        [MinLength(BlindUserConstants.PersonalIDLength, ErrorMessage = BlindUserConstants.PersonalIDLengthError)]
        [MaxLength(BlindUserConstants.PersonalIDLength, ErrorMessage = BlindUserConstants.PersonalIDLengthError)]
        public string PersonalId { get; set; } = null!;

        [Required(ErrorMessage = BlindUserConstants.RequiredTELKIDError)]
        public string TELKID { get; set; } = null!;

        public string? BirthDate { get; set; }

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Street { get; set; } = null!;

        [Required]
        public int StreetNumber { get; set; }

        public List<CityViewModel>? CityNames { get; set; }
    }
}
