using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendPro
{
    public interface ISendProService<TConfiguration,TSendItem>
    {
        bool Send(TSendItem tSendItem);
        Task<bool> SendAsync(TSendItem tSendItem);
    }
}
