﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator.EmailSettings
{
    public class EmailViewModel
    {
        public string Id { get; set; }
        public string BlindUserId { get; set; }

        public string Email { get; set; }

        public string EmailProvider { get; set; }
    }
}
