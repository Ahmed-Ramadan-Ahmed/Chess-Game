using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class Bishop : BaseChassPiece
    {
        public Bishop(Colors color) : base(color, Types.Bishop)
        {
            Movements.Add(Movement.InfUpLeft());
            Movements.Add(Movement.InfUpRight());
            Movements.Add(Movement.InfDownLeft());
            Movements.Add(Movement.InfDownRight());
        }
    }
}
