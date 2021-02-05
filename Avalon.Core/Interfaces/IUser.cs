using Avalon.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IUser
    {
        string Name { get; }

        Role Role { get; set; }

        Task SendMessage(string message);

        Task<IEnumerable<IUser>> SelectUsers(int count);

        Task<QuestCard> SelectQuestCard();

        Task<VoteToken> SelectVoteToken();
    }
}
