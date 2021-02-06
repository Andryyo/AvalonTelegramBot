using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class User : IUser
    {
        private readonly IUserInteractionService userInteractionService;

        public User(IUserInteractionService userInteractionService)
        {
            this.userInteractionService = userInteractionService;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public Role? Role { get; set; }

        public Task<QuestCard> SelectQuestCard() => userInteractionService.SelectQuestCard(Id);

        public Task<IEnumerable<IUser>> SelectUsers(IEnumerable<IUser> users, int count) => userInteractionService.SelectUsers(Id, users, count);

        public Task<VoteToken> SelectVoteToken() => userInteractionService.SelectVoteToken(Id);

        public Task SendMessage(string message) => userInteractionService.SendMessage(Id, message);
    }
}
