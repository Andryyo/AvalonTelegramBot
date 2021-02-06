using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    interface IAvalonGame
    {
        int Id { get; set; }

        IList<IUser> Users { get; set; }

        Task Run();
    }
}
