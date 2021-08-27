using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Models
{
    public class PlayerGrid : Grid
    {
        public Guid PlayerId { get; init; }
        public IEnumerable<ShipSquare> Ships { get; init; }
    }
}
