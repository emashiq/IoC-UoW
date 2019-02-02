using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendPro
{
    public class Email : IEmail
    {
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Body { get ; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
