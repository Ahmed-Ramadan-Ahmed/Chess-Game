using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class Knight: BaseChassPiece
    {

        public Knight(Colors color) : base(color, Types.Knight)
        {
            Movements.AddRange(Movement.KnightMovement());
        }
    }
}
