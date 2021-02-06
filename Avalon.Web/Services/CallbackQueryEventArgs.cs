using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Web.Services
{
    public class CallbackQueryEventArgs : EventArgs
    {
        public CallbackQueryEventArgs(long messageId, string data)
        {
            MessageId = messageId;
            Data = data;
        }

        public long MessageId { get; }

        public string Data { get; }
    }
}
