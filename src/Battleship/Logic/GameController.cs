using Battleship.Core.Enums;
using Battleship.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic
{
    public class GameController
    {
        public GameStatus PrepareNewGame()
        {
            return new GameStatus();
        }

        public ShotResponse Shoot(Point coordinates)
        {
            return new ShotResponse(new(1,1), ShotResult.Hit, "none");
        }
    }
}
