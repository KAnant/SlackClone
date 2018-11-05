using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApi.Data.Entities
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<Email>();
            FromAddresses = new List<Email>();
        }

        public List<Email> ToAddresses { get; set; }
        public List<Email> FromAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
