using Avalon.Core.Interfaces;
using Avalon.Core.Models;
using Avalon.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Web.Services
{
    public class GamesManager : IGamesManager
    {
        public GamesManager(
            IUserInteractionService userInteractionService)
        {
            this.userInteractionService = userInteractionService;
        }

        private const int MinPlayersCount = 5;
        private List<IAvalonGame> games = new List<IAvalonGame>();
        private readonly IUserInteractionService userInteractionService;

        public IEnumerable<IAvalonGame> Games => games;

        public async Task Create(long id)
        {
            var game = new AvalonGame(userInteractionService);
            game.Id = id;

            games.Add(game);

            await userInteractionService.SendMessage(id, "Game was created. Call /join command to join, /play to start game");
        }

        public async Task Join(long id, IUser user)
        {
            var game = games.FirstOrDefault(x => x.Id == id);
            if (game != null && game.State == Core.Enums.GameState.Created)
            {
                game.Users.Add(user);

                await userInteractionService.SendMessage(id, string.Format("{0} joined the game", user.Name));
            }
        }

        public async Task Play(long id)
        {
            var game = games.FirstOrDefault(x => x.Id == id);
            if (game != null && game.State == Core.Enums.GameState.Created)
            {
                var random = new Random();

                int i = 0;
                while (game.Users.Count < MinPlayersCount)
                {
                    var user = new DummyUser(random);
                    user.Name = string.Format("Dummy {0}", ++i);
                    user.Id = i;
                    game.Users.Add(user);

                    await userInteractionService.SendMessage(id, string.Format("{0} joined the game", user.Name));
                }

                _ = Task.Run(async () =>
                {
                    await userInteractionService.SendMessage(id, "Game is starting");

                    await game.Run();
                    games.Remove(game);

                    await userInteractionService.SendMessage(id, "Game ended");
                });
            }
        }
    }
}
