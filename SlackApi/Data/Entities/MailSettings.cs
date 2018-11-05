using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApi.Data.Entities
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string AuthEmail { get; set; }
        public string AuthPassword { get; set; }
    }
}
