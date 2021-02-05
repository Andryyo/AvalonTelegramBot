using Avalon.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class EndPhase : IPhase
    {
        public EndPhase(IAvalonContext context)
        {
            this.Context = context;
        }

        public IAvalonContext Context { get; set; }

        public async Task<IPhase> Execute()
        {
            var target = (await Context.Users.FirstOrDefault(x => x.Role == Enums.Role.Assassin).SelectUsers(1)).Single();

            if (target.Role == Enums.Role.Merlin)
            {
                return new EndPhase(Context);
            }
            else
            {
                return new EndPhase(Context);
            }
        }
    }
}
