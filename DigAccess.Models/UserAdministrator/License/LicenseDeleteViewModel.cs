﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator.License
{
    public class LicenseDeleteViewModel
    {
        public string Id { get; set; }

        public string BlindUserId { get; set; }

        public string DateFrom { get; set; }

        public string MACAddress { get; set; }
    }
}