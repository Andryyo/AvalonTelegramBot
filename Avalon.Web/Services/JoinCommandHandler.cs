using Avalon.Core.Interfaces;
using Avalon.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Avalon.Web.Services
{
    public class JoinCommandHandler : ICommandHandler
    {
        private readonly IUserInteractionService userInteractionService;
        private readonly IGamesManager gamesManager;

        public JoinCommandHandler(
            IUserInteractionService userInteractionService,
            IGamesManager gamesManager)
        {
            this.userInteractionService = userInteractionService;
            this.gamesManager = gamesManager;
        }

        public string Command => "/join";

        public async Task Handle(Message message)
        {
            if (message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
            {
                await userInteractionService.SendMessage(message.Chat.Id, "You should add bot to a group");
                return;
            }

            var user = new Core.Models.User(userInteractionService);
            user.Id = message.From.Id;
            user.Name = message.From.Username;

            await gamesManager.Join(message.Chat.Id, user);
        }
    }
}
