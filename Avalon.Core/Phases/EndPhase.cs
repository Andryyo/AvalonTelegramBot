using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Phases
{
    public class EndPhase : IPhase
    {
        public EndPhase(
            IAvalonContext context)
        {
            Context = context;
        }

        public IAvalonContext Context { get; set; }

        public async Task<IPhase> Execute()
        {
            await Context.SendMessage(string.Format("Roles were:\r\n{0}", string.Join("\r\n", Context.Users.Select(u => string.Format("{0} - {1}", u.Name, u.Role)))));

            return new EndPhase(Context);
        }
    }
}
