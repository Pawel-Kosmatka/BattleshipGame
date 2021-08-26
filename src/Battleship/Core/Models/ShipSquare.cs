using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Models
{
    public class ShipSquare
    {
        public Guid ShipId { get; set; }
        public Point Coordinates { get; set; }
        public bool WasHit { get; set; }
    }
}
