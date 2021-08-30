﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic.Models
{
    public record CreatedPlayersStatus(Guid GameId,IEnumerable<AIPlayer> Players){}
}
