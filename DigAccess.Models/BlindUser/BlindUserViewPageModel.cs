using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.BlindUser
{
    public class BlindUserViewPageModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public bool IsProfileActive { get; set; }
    }
}
