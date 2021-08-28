using Battleship.Core.Enums;
using Battleship.Core.Models;
using Battleship.Logic.Models;
using Battleship.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic
{
    public class GameController : IGameController
    {
        private readonly GameContext _context;
        private readonly Random _random;

        public GameController(GameContext context)
        {
            _context = context;
            _random = new Random();
        }

        public GameStatus PrepareNewGame(IEnumerable<PlayerGrid> playerGrids)
        {
            var nextPlayerId = playerGrids.ElementAt(_random.Next(2)).PlayerId;
            var game = new Game() { Grids = playerGrids };
            game.Next = nextPlayerId;

            _context.RegisterNewGame(game);

            return new GameStatus(game.Id, game.Next);
        }

        public ShotResponse Shoot(Point coordinates)
        {
            return new ShotResponse(new(1, 1), ShotResult.Hit, "none");
        }
    }
}
