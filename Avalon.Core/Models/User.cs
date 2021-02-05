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

        public int Id { get; set; }

        public string Name { get; set; }

        public Role? Role { get; set; }

        public Task<QuestCard> SelectQuestCard() => userInteractionService.SelectQuestCard(this);

        public Task<IEnumerable<IUser>> SelectUsers(int count) => userInteractionService.SelectUsers(this, count);

        public Task<VoteToken> SelectVoteToken() => userInteractionService.SelectVoteToken(this);

        public Task SendMessage(string message) => userInteractionService.SendMessage(this, message);
    }
}
