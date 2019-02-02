using System.Net.Mail;
using System.Text;

namespace SendPro
{
    public interface IEmailConfiguration
    {
        string EmailAccount { get; set; }
        string Password { get; set; }
        int Port { get; set; }
        SmtpDeliveryMethod DeliveryMethod { get; set; }
        bool UseDefaultCredentials { get; set; }
        string Host { get; set; }
        int Timeout { get; set; }
        Encoding BodyEncoding { get; set; }
        bool IsBodyHtml { get; set; }
        DeliveryNotificationOptions DeliveryNotificationOptions { get; set; }
    }
}
