using Avalon.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avalon.Core.Interfaces
{
    public interface IAvalonContext
    {
        IList<IUser> Users { get; }

        IUser Leader { get; set; }

        IList<QuestCard> Results { get; set; }

        Task SendMessage(string message);
    }
}
