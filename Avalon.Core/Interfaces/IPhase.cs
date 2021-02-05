using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IPhase
    {
        IAvalonContext Context { get; }

        Task<IPhase> Execute();
    }
}
