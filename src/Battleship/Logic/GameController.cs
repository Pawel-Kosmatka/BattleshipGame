using Battleship.Core.Enums;
using Battleship.Core.Extensions;
using Battleship.Core.Models;
using Battleship.Logic.Models;
using Battleship.Logic.StatusNotification;
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
        private readonly StatusNotificator _notificator;

        public GameController(GameContext context, StatusNotificator notificator)
        {
            _context = context;
            _random = new Random();
            _notificator = notificator;
        }

        public GameStatus PrepareNewGame(IEnumerable<PlayerGrid> playerGrids)
        {
            var nextPlayerId = playerGrids.ElementAt(_random.Next(2)).PlayerId;
            var game = new Game() { Grids = playerGrids };

            _context.RegisterNewGame(game);
            _context.SetNextPlayer(game.Id, nextPlayerId);

            var status = new GameStatus(game.Id, game.Next);

            NotifyAboutStatusUpdate(status);

            return status;
        }

        public GameStatus TakeAShot(Guid gameId, Guid shooterId, Point coordinates)
        {
            var game = _context.GetGame(gameId);
            var targetedGrid = game.Grids.FirstOrDefault(g => g.PlayerId != shooterId);

            var shotResponse = targetedGrid.Squares.TargetSquare(coordinates);
            var nextPlayer = targetedGrid.PlayerId;

            _context.UpdatePlayerGrid(gameId, shooterId, coordinates, shotResponse == ShotResult.Hit);
            _context.SetNextPlayer(game.Id, nextPlayer);

            var responseText = string.Empty;

            if (shotResponse == ShotResult.Hit)
            {
                var ship = targetedGrid.Ships.FirstOrDefault(s => s.Coordinates == coordinates);

                var shipSunk = !targetedGrid.Ships
                    .Where(s => s.ShipId == ship.ShipId)
                    .Any(s => s.WasHit == false);

                if (shipSunk)
                {
                    shotResponse = ShotResult.Sink;
                    responseText = $"{ship.ShipName} sink";
                }
            }

            if (shotResponse == ShotResult.Sink)
            {
                var allSunk = !targetedGrid.Ships.Any(s => s.WasHit == false);

                if (allSunk && game.IsFirstShotInThisRound == false)
                {
                    shotResponse = ShotResult.GameOver;
                    responseText = responseText += ", that was the last one!";
                    _context.RemoveGame(game.Id);
                }

                shotResponse = ShotResult.FirstFleetSunk;
                responseText = responseText += ", that was the last one!";
            }

            var response = new ShotResponse(coordinates, shotResponse, responseText, shooterId);
            var newStatus = new GameStatus(gameId, nextPlayer, response);

            NotifyAboutStatusUpdate(newStatus);

            return newStatus;
        }

        public IDisposable SubscribeForStatusUpdates(IObserver<GameStatus> observer)
        {
            return _notificator.Subscribe(observer);
        }

        private void NotifyAboutStatusUpdate(GameStatus status)
        {
            _notificator.NotifyObservers(status);
        }

        public Guid GetNextPlayerId(Guid id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);
            return game.Next;
        }
    }
}
