using Battleship.Core.Enums;
using Battleship.Core.Models;

namespace Battleship.Core.Extensions
{
    public static class GridSquaresExtensions
    {
        public static void SetAt(this bool?[,] squares, Point coordinates, bool? value)
        {
            squares[coordinates.X, coordinates.Y] = value;
        }
        public static bool? ValueAt(this bool?[,] squares, Point coordinates)
        {
            return squares[coordinates.X, coordinates.Y];
        }
        public static ShotResult TargetSquare(this bool?[,] squares, Point coordinates)
        {
            return squares.ValueAt(coordinates) switch
            {
                null => ShotResult.Miss,
                true => ShotResult.Hit,
                _ => ShotResult.Miss
            };
        }
    }
}
