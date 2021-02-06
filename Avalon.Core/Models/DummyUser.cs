using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class DummyUser : IUser
    {
        private readonly Random random;

        public DummyUser(Random random)
        {
            this.random = random;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public Role? Role { get; set; }

        public async Task<QuestCard> SelectQuestCard()
        {
            switch (Role)
            {
                case Enums.Role.Assassin:
                case Enums.Role.Minion:
                    return random.Next(2) == 1 ? QuestCard.MissionSuccess : QuestCard.MissionFailed;
                case Enums.Role.Merlin:
                case Enums.Role.Servant:
                default:
                    return QuestCard.MissionSuccess;
            }
        }

        public async Task<IEnumerable<IUser>> SelectUsers(IEnumerable<IUser> users, int count)
        {
            return users.Select(x => new { User = x, Value = random.Next() }).OrderBy(x => x.Value).Take(count).Select(x => x.User);
        }

        public async Task<VoteToken> SelectVoteToken() => random.Next(2) == 1 ? VoteToken.VoteApproved : VoteToken.VoteRejected;

        public async Task SendMessage(string message)
        {
        }
    }
}
