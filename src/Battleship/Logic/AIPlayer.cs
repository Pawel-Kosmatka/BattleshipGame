using Battleship.Core.Extensions;
using Battleship.Core.Models;
using Battleship.Core.Settings;
using Battleship.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic
{
    public class AIPlayer
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; init; }
        public PlayerGrid OceanGrid { get; private set; }
        public Grid TrackingGrid { get; } = new Grid();

        public Point TakeATarget(Random random)
        {
            throw new NotImplementedException();
        }

        public void HandleShotResponse(ShotResponse response)
        {

        }

        public PlayerGrid PositionFleet(Random random)
        {
            var ships = GenerateFleet(random);
            OceanGrid = new PlayerGrid
            {
                PlayerId = Id,
                Ships = ships
            };

            foreach (var shipSquare in ships)
            {
                OceanGrid.Squares.SetAt(shipSquare.Coordinates, true);
            }

            return OceanGrid;
        }
        private static IEnumerable<ShipSquare> GenerateFleet(Random random)
        {
            var ships = new List<ShipSquare>();

            foreach (var (name, length) in GameSettings.Ships)
            {
                var newShip = GenrateNewShip(random, length, name, ships);
                ships.AddRange(newShip);
            }

            return ships;
        }

        private static IEnumerable<ShipSquare> GenrateNewShip(Random random, int shipLength, string name, List<ShipSquare> existingShips)
        {
            IEnumerable<ShipSquare> ship;
            var shipIsFitting = false;
            do
            {
                ship = GenerateCoordinates(random, shipLength, name);
                if (IsNotFittingGrid(ship)) continue;
                if (IsOverlapping(ship, existingShips)) continue;
                shipIsFitting = true;
            } while (!shipIsFitting);

            return ship;
        }

        private static bool IsOverlapping(IEnumerable<ShipSquare> ship, List<ShipSquare> existingShips)
        {
            foreach (var square in ship)
            {
                if (existingShips.Any(s => s.Coordinates == square.Coordinates))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsNotFittingGrid(IEnumerable<ShipSquare> ship)
        {
            return ship.Any(c => c.Coordinates.X >= GameSettings.XLength || c.Coordinates.Y >= GameSettings.YLength);
        }

        private static IEnumerable<ShipSquare> GenerateCoordinates(Random random, int shipLength, string name)
        {
            var coordinates = new List<ShipSquare>();
            var shipId = Guid.NewGuid();
            var isPlacedHorizontally = random.Next(2) == 0;

            var head = new ShipSquare
            {
                Coordinates = new(random.Next(GameSettings.XLength), random.Next(GameSettings.YLength)),
                ShipId = shipId,
                ShipName = name
            };

            coordinates.Add(head);

            for (int i = 1; i < shipLength; i++)
            {
                coordinates.Add(NextCoordinatesFromHead(head, isPlacedHorizontally, i, name, shipId));
            }

            return coordinates;
        }

        private static ShipSquare NextCoordinatesFromHead(ShipSquare head, bool isPlacedHorizontally, int squaresFromHead, string name, Guid shipId)
        {
            if (isPlacedHorizontally)
            {
                return new ShipSquare
                {
                    Coordinates = new(head.Coordinates.X + squaresFromHead, head.Coordinates.Y),
                    ShipId = shipId,
                    ShipName = name,
                };
            }
            return new ShipSquare
            {
                Coordinates = new(head.Coordinates.X, head.Coordinates.Y + squaresFromHead),
                ShipId = shipId,
                ShipName = name,
            };
        }
    }
}
