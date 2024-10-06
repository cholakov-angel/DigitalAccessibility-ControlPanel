using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Entities
{
    public class MasterKey
    {
        public MasterKey()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Required] 
        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Required] 
        public string MasterLicence { get; set; } = null!;
    }
}
