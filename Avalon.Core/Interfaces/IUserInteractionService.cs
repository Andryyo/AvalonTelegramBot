using Avalon.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IUserInteractionService
    {
        Task SendMessage(string message);

        Task SendMessage(IUser user, string message);

        Task<IEnumerable<IUser>> SelectUsers(IUser user, int count);

        Task<QuestCard> SelectQuestCard(IUser user);

        Task<VoteToken> SelectVoteToken(IUser user);
    }
}
