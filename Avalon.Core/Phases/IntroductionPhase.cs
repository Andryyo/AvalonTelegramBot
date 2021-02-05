using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class IntroductionPhase : IPhase
    {
        public IAvalonContext Context { get; }

        public async Task<IPhase> Execute()
        {
            var evilCharacters = Context.Users.Where(x => x.Role == Role.Assassin || x.Role == Role.Minion);

            var merlin = Context.Users.FirstOrDefault(x => x.Role == Role.Merlin);

            foreach (var user in evilCharacters)
            {
                await user.SendMessage(string.Format("Your evil bros are {0}", string.Join(",", evilCharacters.Where(x => x != user).Select(x => x.Name))));
            }

            await merlin.SendMessage(string.Format("Evil guys are {0}", string.Join(",", evilCharacters.Select(x => x.Name))));

            return new TeamBuildingPhase(Context, 1, 0); 
        }
    }
}
