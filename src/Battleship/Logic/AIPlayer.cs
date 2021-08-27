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
        public Guid Id { get; }
        public string Name { get; }
        public PlayerGrid OceanGrid { get; private set; }
        public Grid TrackingGrid { get; } = new Grid();

        public Point TakeAShot(Random random)
        {
            throw new NotImplementedException();
        }

        public void HandleShotResponse(ShotResponse response)
        {

        }

        public PlayerGrid PositionShips(Random random)
        {
            var ships = GenerateRandomShips(random);
            OceanGrid = new PlayerGrid { PlayerId = Id, Ships = ships };

            foreach (var shipSquare in ships)
            {
                OceanGrid.Squares[shipSquare.Coordinates.X, shipSquare.Coordinates.Y] = true;
            }

            return OceanGrid;
        }
        private IEnumerable<ShipSquare> GenerateRandomShips(Random random)
        {
            throw new NotImplementedException();

        }
    }
}
