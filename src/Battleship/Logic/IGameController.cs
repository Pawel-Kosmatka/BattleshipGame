using Battleship.Core.Models;
using Battleship.Logic.Models;
using System;
using System.Collections.Generic;

namespace Battleship.Logic
{
    public interface IGameController
    {
        GameStatus PrepareNewGame(IEnumerable<PlayerGrid> playerGrids);
        GameStatus TakeAShot(Guid gameId, Guid shooterId, Point coordinates);
        IDisposable SubscribeForStatusUpdates(IObserver<GameStatus> observer);
        Guid GetNextPlayerId(Guid id);
    }
}