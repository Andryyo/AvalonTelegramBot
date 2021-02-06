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

        public async Task<QuestCard> SelectQuestCard(long id)
        {
            await Task.Delay(2000);

            return QuestCard.MissionSuccess;
        }

        public async Task<IEnumerable<IUser>> SelectUsers(long id, IEnumerable<IUser> users, int count)
        {
            await Task.Delay(2000);

            return users.Select(x => new { User = x, Value = new Random().Next() }).OrderBy(x => x.Value).Take(count).Select(x => x.User);
        }

        public async Task<VoteToken> SelectVoteToken(long id)
        {
            await Task.Delay(2000);

            return VoteToken.VoteApproved;
        }

        public async Task SendMessage(long id, string message)
        {
            await Task.Delay(2000);

            await client.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(id), message);
        }
    }
}
