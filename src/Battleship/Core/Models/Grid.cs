using Battleship.Core.Settings;

namespace Battleship.Core.Models
{
    public class Grid
    {
        public bool?[,] Squares { get; } = new bool?[GameSettings.XLength, GameSettings.YLength];
    }
}
