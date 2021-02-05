using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class AvalonContext : IAvalonContext
    {
        private readonly IUserInteractionService userInteractionService;

        public AvalonContext(IUserInteractionService userInteractionService)
        {
            this.userInteractionService = userInteractionService;
        }

        public IList<IUser> Users { get; set; } = new List<IUser>();

        public IUser Leader { get; set; }

        public IList<QuestCard> Results { get; set; } = new List<QuestCard>();

        public Task SendMessage(string message) => userInteractionService.SendMessage(message);
    }
}
