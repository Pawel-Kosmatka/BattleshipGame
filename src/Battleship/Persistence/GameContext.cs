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
    }
}
