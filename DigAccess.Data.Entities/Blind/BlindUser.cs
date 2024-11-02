using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace DigAccess.Data.Entities.Blind
{
    public class BlindUser
    {
        [Key]
        public Guid Id { get; set; }

        [PersonalData]
        [MaxLength(60)]
        public string? FirstName { get; set; }

        [PersonalData]
        [MaxLength(60)]
        public string? MiddleName { get; set; }

        [PersonalData]
        [MaxLength(60)]
        public string? LastName { get; set; }

        [PersonalData]
        [MaxLength(10)]
        public string? PersonalId { get; set; }

        [PersonalData]
        public string? TELKNumber { get; set; }

        [PersonalData]
        [MaxLength(10)]
        public string? Phone { get; set; }

        [PersonalData]
        public DateTime? Birthdate { get; set; }

        [Required]
        public string AdministratorId { get; set; } = null!;

        [ForeignKey(nameof(AdministratorId))]
        public ApplicationUser AdministratorUser { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        [PersonalData]
        public Gender Gender { get; set; }

        public ICollection<BlindUserLicence> BlindUserLicences { get; set; } = new List<BlindUserLicence>();

        public ICollection<BlindUserLog> BlindUserLogs { get; set; } = new List<BlindUserLog>();
    }
}
