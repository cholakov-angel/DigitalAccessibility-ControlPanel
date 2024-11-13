using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.OfficeWorker
{
    public class UserDetailsViewModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? PersonalId { get; set; }

        public string? Gender { get; set; }
    }
}
