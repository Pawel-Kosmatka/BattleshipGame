using Battleship.Core.Enums;

namespace Battleship.Core.Models
{
    public record ShotResponse (Point Cooridnates, ShotResult Status, string Message);
}