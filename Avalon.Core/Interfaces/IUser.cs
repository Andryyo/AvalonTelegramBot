using Avalon.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IUser
    {
        long Id { get; }

        string Name { get; }

        Role? Role { get; set; }

        Task SendMessage(string message);

        Task<IEnumerable<IUser>> SelectUsers(IEnumerable<IUser> users, int count);

        Task<QuestCard> SelectQuestCard();

        Task<VoteToken> SelectVoteToken();
    }
}
