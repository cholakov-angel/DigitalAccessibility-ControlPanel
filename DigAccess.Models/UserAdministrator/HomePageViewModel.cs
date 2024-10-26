using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator
{
    public class HomePageViewModel
    {
        public ICollection<BlindUserHomePageViewModel> BlindUsers { get; set; }
    }
}
