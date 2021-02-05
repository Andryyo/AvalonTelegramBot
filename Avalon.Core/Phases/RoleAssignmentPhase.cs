using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    class RoleAssignmentPhase : IPhase
    {
        private readonly IRolesGenerator rolesGenerator;

        public RoleAssignmentPhase(
            IAvalonContext avalonContext,
            IRolesGenerator rolesGenerator)
        {
            this.Context = avalonContext;
            this.rolesGenerator = rolesGenerator;
        }

        public IAvalonContext Context { get; }

        public async Task<IPhase> Execute()
        {
            var rand = new Random((int)DateTime.Now.Ticks);

            var roles = rolesGenerator.Generate(Context.Users.Count()).OrderBy(x => rand.Next()).ToArray();

            int i = 0;
            foreach (var user in Context.Users)
            {
                user.Role = roles[i++];
                await SendRole(user);
            }

            Context.Leader = Context.Users.ElementAt(rand.Next() % Context.Users.Count());
            
            return new IntroductionPhase();
        }

        private async Task SendRole(IUser user)
        {
            switch (user.Role)
            {
                case Role.Minion:
                    await user.SendMessage("You are minion");
                    break;
                case Role.Servant:
                    await user.SendMessage("You are servant");
                    break;
                case Role.Merlin:
                    await user.SendMessage("You are merlin");
                    break;
                case Role.Assassin:
                    await user.SendMessage("You are assassin");
                    break;
                default:
                    await user.SendMessage("You are noone");
                    break;
            }
        }
    }
}
