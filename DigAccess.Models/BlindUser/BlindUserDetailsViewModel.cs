using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.BlindUser
{
    public class BlindUserDetailsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PersonalId { get; set; }

        public string TELKID { get; set; }

        public string? BirthDate { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

    }
}
