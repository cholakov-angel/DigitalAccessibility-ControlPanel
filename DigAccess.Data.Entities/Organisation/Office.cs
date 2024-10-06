using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities.Address;

namespace DigAccess.Data.Entities.Organisation.Organisation
{
    public class Office
    {
        public Office()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public Guid OrganisationId { get; set; }

        [ForeignKey(nameof(OrganisationId))]
        public OrganisationCompany Organisation { get; set; }

        [Required]
        [MaxLength(10)]
        public string LocalPhone { get; set; } = null!;

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
    }
}
