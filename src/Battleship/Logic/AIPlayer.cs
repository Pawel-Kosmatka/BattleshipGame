using Battleship.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic
{
    public class AIPlayer
    {
        public IEnumerable<ShipSquare> Ships { get; }
        public Grid OceanGrid { get; } = new Grid();
        public Grid TrackingGrid { get; } = new Grid();

        public Point TakeAShot(Random random)
        {
            throw new NotImplementedException();
        }

        public void HandleShotResponse(ShotResponse response)
        {

        }

        public IEnumerable<ShipSquare> GenerateRandomShips(Random random)
        {
            throw new NotImplementedException();

        }
    }
}
