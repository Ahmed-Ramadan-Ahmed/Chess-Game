using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class King : BaseChassPiece
    {
        public bool IsFirstMove;  // use in castling
        public King(Colors color) : base(color, Types.King)
        {
            Movements.Add(Movement.Up());
            Movements.Add(Movement.Down());
            Movements.Add(Movement.Left());
            Movements.Add(Movement.Right());
            Movements.Add(Movement.DownLeft());
            Movements.Add(Movement.DownRight());
            Movements.Add(Movement.UpLeft());
            Movements.Add(Movement.UpRight());

            IsFirstMove = true;
        }
    }
}
