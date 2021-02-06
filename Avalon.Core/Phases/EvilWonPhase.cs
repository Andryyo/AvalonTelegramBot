using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Phases
{
    public class EvilWonPhase : IPhase
    {
        public EvilWonPhase(IAvalonContext context)
        {
            this.Context = context;
        }

        public IAvalonContext Context { get; set; }

        public async Task<IPhase> Execute()
        {
            await Context.SendMessage("Evil won");

            return new EndPhase(Context);
        }
    }
}
