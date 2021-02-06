using Avalon.Core.Interfaces;
using Avalon.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Avalon.Web.Services
{
    public class StartCommandHandler : ICommandHandler
    {
        private readonly IUserInteractionService userInteractionService;

        public StartCommandHandler(
            IUserInteractionService userInteractionService)
        {
            this.userInteractionService = userInteractionService;
        }

        public string Command => "/start";

        public async Task Handle(Message message)
        {
            if (message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
            {
                await userInteractionService.SendMessage(message.Chat.Id, "You should add got to a group");
            }
            else
            {
                await userInteractionService.SendMessage(message.Chat.Id, "Hi all");
            }
        }
    }
}
