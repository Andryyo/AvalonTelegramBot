using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Avalon.Web.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly ITelegramBotClient client;

        public UserInteractionService(
            ITelegramBotClient client)
        {
            this.client = client;
        }

        public Task<QuestCard> SelectQuestCard(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> SelectUsers(long id, IEnumerable<IUser> users, int count)
        {
            throw new NotImplementedException();
        }

        public Task<VoteToken> SelectVoteToken(long id)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessage(long id, string message)
        {
            await client.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(id), message);
        }
    }
}
