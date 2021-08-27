using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Models
{
    public class Game
    {
        public Guid Id { get; } = Guid.NewGuid();
        public IEnumerable<PlayerGrid> Grids { get; init; }
        public int Round { get; private set; }
    }
}
