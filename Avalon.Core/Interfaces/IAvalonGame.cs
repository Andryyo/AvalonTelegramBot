using Avalon.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IAvalonGame
    {
        long Id { get; set; }

        GameState State { get; set; }

        IList<IUser> Users { get; set; }

        Task Run();
    }
}
