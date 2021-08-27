using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Models
{
    public class ShipSquare
    {
        public Guid ShipId { get; init; }
        public Point Coordinates { get; init; }
        public bool WasHit { get; private set; } = false;

        public void Hit()
        {
            WasHit = true;
        }
    }
}
