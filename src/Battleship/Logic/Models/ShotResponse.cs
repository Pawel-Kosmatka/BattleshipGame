using Battleship.Core.Enums;
using Battleship.Core.Models;
using System;

namespace Battleship.Logic.Models
{
    public record ShotResponse (Point Cooridnates, ShotResult Status, string Message, Guid Shooter);
}