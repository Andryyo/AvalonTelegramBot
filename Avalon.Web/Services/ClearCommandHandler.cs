using Avalon.Core.Interfaces;
using Avalon.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Avalon.Web.Services
{
    public class ClearCommandHandler : ICommandHandler
    {
        private readonly IUserInteractionService userInteractionService;
        private readonly IGamesManager gamesManager;

        public ClearCommandHandler(
            IUserInteractionService userInteractionService,
            IGamesManager gamesManager)
        {
            this.userInteractionService = userInteractionService;
            this.gamesManager = gamesManager;
        }

        public string Command => "/clear";

        public async Task Handle(Message message)
        {
            //if (message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
            //{
            //    await userInteractionService.SendMessage(message.Chat.Id, "You should add bot to a group");
            //    return;
            //}

            await gamesManager.Clear(message.Chat.Id);
        }
    }
}
