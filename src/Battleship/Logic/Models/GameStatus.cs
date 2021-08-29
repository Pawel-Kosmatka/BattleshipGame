using System;

namespace Battleship.Logic.Models
{
    public record GameStatus(Guid GameId, Guid Next, ShotResponse ShotResponse = null){}
}