using Avalon.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IPhase
    {
        IAvalonContext Context { get; }

        Task<IPhase> Execute();
    }
}
