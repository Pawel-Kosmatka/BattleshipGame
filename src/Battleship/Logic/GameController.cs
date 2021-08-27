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

        public GameController(GameContext context)
        {
            _context = context;
        }

        public GameStatus PrepareNewGame(IEnumerable<PlayerGrid> playerGrids)
        {
            var game = new Game() { Grids = playerGrids };

            _context.RegisterNewGame(game);

            return new GameStatus() { GameId = game.Id};
        }

        public ShotResponse Shoot(Point coordinates)
        {
            return new ShotResponse(new(1, 1), ShotResult.Hit, "none");
        }
    }
}
