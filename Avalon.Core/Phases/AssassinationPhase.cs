using Avalon.Core.Interfaces;
using Avalon.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Core.Phases
{
    public class AssassinationPhase : IPhase
    {
        public AssassinationPhase(
            IAvalonContext context)
        {
            Context = context;
        }

        public IAvalonContext Context { get; }

        public async Task<IPhase> Execute()
        {
            var asassin = Context.Users.FirstOrDefault(x => x.Role == Enums.Role.Assassin);

            await asassin.SendMessage("Who you want to kill?");

            var target = (await asassin.SelectUsers(Context.Users, 1)).Single();

            await Context.SendMessage(string.Format("Asassin killed {0}, he was {1}", target.Name, target.Role));

            if (target.Role == Enums.Role.Merlin)
            {
                return new EvilWonPhase(Context);
            }
            else
            {
                return new GoodWonPhase(Context);
            }
        }
    }
}
