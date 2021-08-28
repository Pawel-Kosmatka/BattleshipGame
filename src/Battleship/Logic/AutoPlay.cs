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
        public IDictionary<Guid, (AIPlayer, AIPlayer)> Games { get; } = new Dictionary<Guid, (AIPlayer, AIPlayer)>();

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
                playerOne.PositionFleet(_random),
                playerTwo.PositionFleet(_random)
            };

            var status = _gameController.PrepareNewGame(grids);

            Games.Add(status.GameId, (playerOne,playerTwo));
        }
    }
}
