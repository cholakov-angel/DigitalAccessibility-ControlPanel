using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities.Organisation.Organisation;
using Microsoft.AspNetCore.Identity;

namespace DigAccess.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? MiddleName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "CHAR(10)")]
        [MaxLength(10)]
        public string? PersonalId { get; set; }

        public Guid? OfficeId { get; set; }

        [ForeignKey(nameof(OfficeId))]
        public Office Office { get; set; }

        public string? MasterKey { get; set; }
    }
}
