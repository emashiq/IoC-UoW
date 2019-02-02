using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendPro
{
    public interface IEmail
    {
        string SendTo { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        List<Attachment> Attachments { get; set; }
    }
}
