using SendPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace IoC_UoW.Models
{
    public class EmailConfiguration: IEmailConfiguration
    {
        public string EmailAccount { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string Host { get; set; }
        public int Timeout { get; set; }
        public Encoding BodyEncoding { get; set; }
        public bool IsBodyHtml { get; set; }
        public DeliveryNotificationOptions DeliveryNotificationOptions { get; set; }
    }
}