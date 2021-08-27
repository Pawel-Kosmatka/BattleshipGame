using Battleship.Core.Enums;
using Battleship.Core.Models;

namespace Battleship.Logic.Models
{
    public record ShotResponse (Point Cooridnates, ShotResult Status, string Message);
}