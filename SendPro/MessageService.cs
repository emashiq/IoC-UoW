using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendPro
{
    public class MessageService<TConfiguration, TSendItem> :ISendProService<TConfiguration, TSendItem>
    {
        private readonly TConfiguration _tConfiguration;
        public MessageService(TConfiguration tConfiguration)
        {
            _tConfiguration = tConfiguration;
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
