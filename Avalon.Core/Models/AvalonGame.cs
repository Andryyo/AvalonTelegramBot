using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class AvalonGame : IAvalonGame
    {
        private readonly IUserInteractionService userInteractionService;

        public AvalonGame(IUserInteractionService userInteractionService)
        {
            this.userInteractionService = userInteractionService;
        }

        public long Id { get; set; }

        public IList<IUser> Users { get; set; } = new List<IUser>();

        public GameState State { get; set; } = GameState.Created;

        public async Task Run()
        {
            State = GameState.InProgress;

            var context = new AvalonContext(userInteractionService);
            context.Users = Users;
            context.Id = Id;

            IPhase state = new RoleAssignmentPhase(context);
            while (state != null)
            {
                state = await state.Execute();
            }

            State = GameState.Ended;
        }
    }
}
