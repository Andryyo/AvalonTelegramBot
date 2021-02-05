using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Task<IPhase> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
