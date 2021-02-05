using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    interface IAvalonGame
    {
        IList<IUser> Users { get; set; }

        Task Run();
    }
}
