using Avalon.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IUserInteractionService
    {
        Task SendMessage(long id, string message);

        Task<IEnumerable<IUser>> SelectUsers(long id, IEnumerable<IUser> users, int count);

        Task<QuestCard> SelectQuestCard(long id);

        Task<VoteToken> SelectVoteToken(long id);
    }
}
