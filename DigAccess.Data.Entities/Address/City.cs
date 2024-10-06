using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities.Organisation.Organisation;

namespace DigAccess.Data.Entities.Address
{
    public class City
    {
        public City()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Office> Offices { get; set; } = new List<Office>();
    }
}
