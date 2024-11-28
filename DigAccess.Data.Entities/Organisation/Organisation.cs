using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Organisation.Organisation;

namespace DigAccess.Data.Entities.Organisation
{
    public class OrganisationCompany
    {
        public OrganisationCompany()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public string National_Phone { get; set; } = null!;

        public ICollection<Office> Offices { get; set; } = new List<Office>();

        public bool IsDeleted { get; set; }
    }
}
