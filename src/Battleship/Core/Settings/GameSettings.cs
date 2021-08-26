using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Settings
{
    public static class GameSettings
    {
        public static readonly int XLength = 10;
        public static readonly int YLength = 10;

        public static readonly Dictionary<string, int> Ships = new Dictionary<string, int> {
            {"Carrier", 5},
            {"Battleship", 4},
            {"Destroyer", 3},
            {"Submarine", 3},
            {"Patrol Boat", 2}
        };
    }
}
