using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    class RoleAssignmentPhase : IPhase
    {
        public RoleAssignmentPhase(
            IAvalonContext avalonContext)
        {
            this.Context = avalonContext;
        }

        public IAvalonContext Context { get; }

        public async Task<IPhase> Execute()
        {
            await Context.SendMessage("Wait, we`ll assign roles");

            var rand = new Random();

            var roles = GetRoles(Context.Users.Count()).Select(x => new { Role = x, Value = rand.Next() }).OrderBy(x => x.Value).Select(x => x.Role).ToArray();

            int i = 0;
            foreach (var user in Context.Users)
            {
                user.Role = roles[i++];
                await SendRole(user);
            }

            Context.Leader = Context.Users.ElementAt(rand.Next() % Context.Users.Count());

            await Context.SendMessage(string.Format("Leader is {0}", Context.Leader.Name));

            return new IntroductionPhase(Context);
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

        private IEnumerable<Role> GetRoles(int players)
        {
            switch (players)
            {
                case 5:
                    return new Role[] { Role.Merlin, Role.Assassin, Role.Servant, Role.Servant, Role.Minion};
                case 6:
                    return new Role[] { Role.Merlin, Role.Assassin, Role.Servant, Role.Servant, Role.Minion, Role.Servant };
                case 7:
                    return new Role[] { Role.Merlin, Role.Assassin, Role.Servant, Role.Servant, Role.Minion, Role.Servant, Role.Minion };
                case 8:
                    return new Role[] { Role.Merlin, Role.Assassin, Role.Servant, Role.Servant, Role.Minion, Role.Servant, Role.Minion, Role.Servant };
                case 9:
                    return new Role[] { Role.Merlin, Role.Assassin, Role.Servant, Role.Servant, Role.Minion, Role.Servant, Role.Minion, Role.Servant, Role.Servant };
                case 10:
                    return new Role[] { Role.Merlin, Role.Assassin, Role.Servant, Role.Servant, Role.Minion, Role.Servant, Role.Minion, Role.Servant, Role.Servant, Role.Minion};
                default:
                    throw new Exception();
            }
        }
    }
}
