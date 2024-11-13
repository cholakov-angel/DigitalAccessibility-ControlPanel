using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.OfficeWorker
{
    public class WaitingUsersViewModel
    {
        public string UserId { get; set; }

        public string OfficeId { get; set; }

        public List<UserWaiting> UsersWaitings { get; set; } = new List<UserWaiting>();
    }
}
