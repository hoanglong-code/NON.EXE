using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NON.EXE.Models
{
    public class MailSettings
    {
        public string Mail { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }

    }
}
