using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    class AvalonGame : IAvalonGame
    {
        private readonly IUserInteractionService userInteractionService;

        public AvalonGame(IUserInteractionService userInteractionService)
        {
            this.userInteractionService = userInteractionService;
        }

        public IList<IUser> Users { get; set; }

        public async Task Run()
        {
            var context = new AvalonContext(userInteractionService);
            context.Users = Users;

            IPhase state = new RoleAssignmentPhase(context);
            while (state != null)
            {
                state = await state.Execute();
            }
        }
    }
}
