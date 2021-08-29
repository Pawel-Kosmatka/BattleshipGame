using Battleship.Core.Enums;
using Battleship.Core.Models;
using Battleship.Logic.Models;
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
        public IDictionary<Guid, AIPlayer[]> Games { get; } = new Dictionary<Guid, AIPlayer[]>();

        public AutoPlay(IGameController gameController)
        {
            _random = new Random();
            _gameController = gameController;
        }

        public CreatedPlayersStatus CreateNewGame(string playerOneName, string playerTwoName)
        {
            var players = new AIPlayer[] { new AIPlayer { Name = playerOneName }, new AIPlayer { Name = playerTwoName } };

            var grids = new PlayerGrid[]
            {
                players[0].PositionFleet(_random),
                players[1].PositionFleet(_random)
            };

            var status = _gameController.PrepareNewGame(grids);

            Games.Add(status.GameId, players);

            return new CreatedPlayersStatus(players);
        }

        public async Task StartGameAsync(Guid id)
        {
            var players = Games.FirstOrDefault(g => g.Key == id).Value;

            var nextPlayer = _gameController.GetNextPlayerId(id);

            var status = await StartShootingLoopAsync(id, players, nextPlayer);

            GameOver(status);
        }

        private void GameOver(GameStatus status)
        {
            Games.Remove(status.GameId);
        }

        private async Task<GameStatus> StartShootingLoopAsync(Guid gameId, AIPlayer[] players, Guid next)
        {
            AIPlayer nextPlayer;
            GameStatus status;

            do
            {
                nextPlayer = players.First(p => p.Id == next);

                status = Shoot(gameId, nextPlayer);

                await WaitASecondAsync();
            } while (status.ShotResponse.Status != ShotResult.GameOver);

            return status;
        }

        private static async Task WaitASecondAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        private GameStatus Shoot(Guid gameId, AIPlayer player)
        {
            var target = player.TakeATarget(_random);

            var response = _gameController.TakeAShot(gameId, player.Id, target);

            player.HandleShotResponse(response.ShotResponse);

            return response;
        }
    }
}
