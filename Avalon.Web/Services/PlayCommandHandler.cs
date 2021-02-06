using Avalon.Core.Interfaces;
using Avalon.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Avalon.Web.Services
{
    public class PlayCommandHandler : ICommandHandler
    {
        private readonly IUserInteractionService userInteractionService;
        private readonly IGamesManager gamesManager;

        public PlayCommandHandler(
            IUserInteractionService userInteractionService,
            IGamesManager gamesManager)
        {
            this.userInteractionService = userInteractionService;
            this.gamesManager = gamesManager;
        }

        public string Command => "/play";

        public async Task Handle(Message message)
        {
            if (message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
            {
                await userInteractionService.SendMessage(message.Chat.Id, "You should add bot to a group");
                return;
            }

            await gamesManager.Play(message.Chat.Id);
        }
    }
}
