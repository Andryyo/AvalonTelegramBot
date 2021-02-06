using Avalon.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Web.Interfaces
{
    public interface IGamesManager
    {
        IEnumerable<IAvalonGame> Games { get; }

        Task Create(long id);

        Task Join(long id, IUser user);

        Task Play(long id);
    }
}