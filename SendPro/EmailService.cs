using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendPro
{
    public class EmailService<TEmailConfiguration,TSendItem> :ISendProService<TEmailConfiguration,TSendItem>
    {
        private readonly TEmailConfiguration _tEmailConfiguration;
        public EmailService(TEmailConfiguration tEmailConfiguration)
        {
            _tEmailConfiguration = tEmailConfiguration;
        }
        public bool Send(TSendItem email)
        {
            return true;
        }

        public Task<bool> SendAsync(TSendItem email)
        {
            return Task.FromResult(Send(email));
        }
    }
}
