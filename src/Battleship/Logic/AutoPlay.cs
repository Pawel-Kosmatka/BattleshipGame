using Battleship.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic
{
    public class AutoPlay
    {
        private readonly IGameController _gameController;
        private readonly Random _random;
        private IDictionary<Guid, (AIPlayer, AIPlayer)> _games;

        public AutoPlay(IGameController gameController)
        {
            _random = new Random();
            _gameController = gameController;
        }

        public void StartNewGame()
        {
            var playerOne = new AIPlayer();
            var playerTwo = new AIPlayer();

            var grids = new PlayerGrid[]
            {
                playerOne.PositionShips(_random),
                playerTwo.PositionShips(_random)
            };

            var status = _gameController.PrepareNewGame(grids);

            _games.Add(status.GameId, (playerOne,playerTwo));
        }
    }
}
