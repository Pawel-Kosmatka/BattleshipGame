using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Models
{
    public class Game
    {
        private Guid _next;

        public Guid Id { get; } = Guid.NewGuid();
        public IEnumerable<PlayerGrid> Grids { get; init; }
        public int Round { get; private set; }
        public bool IsFirstShotInThisRound { get; private set; }
        public Guid Next 
        { 
            get => _next;
            set
            {
                IsFirstShotInThisRound = !IsFirstShotInThisRound;
                if (IsFirstShotInThisRound == true)
                {
                    Round++;
                }
                _next = value;
            }
        }
    }
}
