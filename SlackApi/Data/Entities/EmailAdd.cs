using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApi.Data.Entities
{
    public class EmailAdd
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
