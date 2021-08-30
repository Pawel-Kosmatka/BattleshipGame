using Battleship.Core.Enums;
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
        public List<Point> LastHits { get; private set; } = new List<Point>();

        public Point TakeATarget(Random random)
        {
            if (LastHits.Count > 0)
            {
                var possibleTargets = GetPossibleTargets();

                var checkedTargets = possibleTargets.Where(c => CheckCoordinates(c));
                
                return checkedTargets.ElementAt(random.Next(checkedTargets.Count()));
            }

            return TargetRandomSquare(random);
        }

        private List<Point> GetPossibleTargets()
        {
            var possibleTargets = new List<Point>();

            if (LastHits.Count > 1)
            {
                var sortedHits = LastHits.OrderBy(c => c.X).ThenBy(c => c.Y);
                var shipStart = sortedHits.First();
                var shipEnd = sortedHits.Last();


                if (shipStart.X - shipEnd.X != 0)
                {
                    possibleTargets = new List<Point> { new Point(shipStart.X - 1, shipStart.Y), new Point(shipEnd.X + 1, shipEnd.Y) };
                }
                else
                {
                    possibleTargets = new List<Point> { new Point(shipStart.X, shipStart.Y - 1), new Point(shipEnd.X, shipEnd.Y + 1) };
                }
            }
            if (LastHits.Count == 1)
            {
                var hit = LastHits.First();

                possibleTargets = new List<Point> { new Point(hit.X - 1, hit.Y), new Point(hit.X + 1, hit.Y), new Point(hit.X, hit.Y - 1), new Point(hit.X, hit.Y + 1) };

            }

            return possibleTargets;
        }

        private Point TargetRandomSquare(Random random)
        {
            Point target;

            do
            {
                target = new Point(random.Next(GameSettings.XLength), random.Next(GameSettings.YLength));
            } while (!CheckCoordinates(target));

            return target;
        }

        private bool CheckCoordinates(Point coordinates)
        {
            if (coordinates.X < 0 || coordinates.X >= GameSettings.XLength)
            {
                return false;
            }
            if (coordinates.Y < 0 || coordinates.Y >= GameSettings.YLength)
            {
                return false;
            }
            if (TrackingGrid.Squares.ValueAt(coordinates) == false)
            {
                return false;
            }
            if (TrackingGrid.Squares.ValueAt(coordinates) == true)
            {
                return false;
            }
            return true;
        }

        public void HandleShotResponse(ShotResponse response)
        {
            if (response.Status == ShotResult.Miss)
            {
                TrackingGrid.Squares.SetAt(response.Cooridnates, false);
                return;
            }
            if (response.Status == ShotResult.Hit)
            {
                TrackingGrid.Squares.SetAt(response.Cooridnates, true);
                LastHits.Add(response.Cooridnates);
                return;
            }
            if (response.Status == ShotResult.Sink)
            {
                TrackingGrid.Squares.SetAt(response.Cooridnates, true);
                LastHits.Clear();
                return;
            }   
            if (response.Status == ShotResult.FirstFleetSunk)
            {
                TrackingGrid.Squares.SetAt(response.Cooridnates, true);
                LastHits.Clear();
                return;
            }
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
