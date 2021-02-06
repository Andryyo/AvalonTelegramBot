using Avalon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Web.Services
{
    public class GamesManager
    {
        private List<IAvalonContext> games = new List<IAvalonContext>();

        public IEnumerable<IAvalonContext> Games => games;

        public async Task Create(int id)
        {

        }
    }
}
