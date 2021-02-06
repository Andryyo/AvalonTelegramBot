using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Avalon.Web.Interfaces
{
    public interface ICommandHandler
    {
        string Command { get; }

        Task Handle(Message message);
    }
}
