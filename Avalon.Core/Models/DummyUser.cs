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
            await Task.Delay(2000);

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
            await Task.Delay(2000);

            return users
                .Where(x => x.Role != Enums.Role.Minion && x.Role != Enums.Role.Assassin)
                .Select(x => new { User = x, Value = random.Next() })
                .OrderBy(x => x.Value)
                .Take(count)
                .Select(x => x.User)
                .ToList();
        }

        public async Task<VoteToken> SelectVoteToken()
        {
            await Task.Delay(2000);

            return random.Next(2) == 1 ? VoteToken.VoteApproved : VoteToken.VoteRejected;
        }

        public async Task SendMessage(string message)
        {
        }
    }
}
