using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.WaitingApproval
{
    public class OrganisationSelectViewModel
    {
        [Required]
        public string OrganisationId { get; set; }

        public List<OrganisationViewModel> Organisations { get; set; } = new List<OrganisationViewModel>();
    }
}
