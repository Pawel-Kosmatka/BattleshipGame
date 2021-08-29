using Battleship.Core.Extensions;
using Battleship.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Persistence
{
    public class GameContext
    {
        public List<Game> Games { get; } = new List<Game>();

        public void RegisterNewGame(Game game)
        {
            Games.Add(game);
        }

        public void RemoveGame(Guid id)
        {
            var game = Games.FirstOrDefault(g => g.Id == id);
            Games.Remove(game);
        }

        public Game GetGame(Guid id)
        {
            var game = Games.FirstOrDefault(g => g.Id == id);
            return game;
        }

        public void SetNextPlayer(Guid gameId, Guid playerId)
        {
            var game = Games.FirstOrDefault(g => g.Id == gameId);
            game.Next = playerId;
        }

        public void UpdatePlayerGrid(Guid gameId, Guid shooterId, Point coordinates, bool value)
        {
            var game = Games.FirstOrDefault(g => g.Id == gameId);
            var grid = game.Grids.FirstOrDefault(g => g.PlayerId != shooterId);

            grid.Squares.SetAt(coordinates, value);
        }
    }
}
