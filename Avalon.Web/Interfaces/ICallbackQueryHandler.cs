using Avalon.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Avalon.Web.Interfaces
{
    public interface ICallbackQueryHandler
    {
        event EventHandler<CallbackQueryEventArgs> CallbackQuery;

        Task Handle(CallbackQuery callbackQuery);
    }
}
