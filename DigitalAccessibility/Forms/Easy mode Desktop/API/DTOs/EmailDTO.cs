using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_mode_Desktop.API.DTOs
{
    public class EmailDTO
    {
        public string email { get; set; }

        public string password { get; set; }

        public string incommingServer { get; set; }

        public string outgoingServer { get; set; }

        public int port { get; set; }
    }
}
