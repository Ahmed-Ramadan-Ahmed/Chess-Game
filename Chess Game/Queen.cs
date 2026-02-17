using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class Queen : BaseChassPiece
    {
        public Queen(Colors color) : base(color, Types.Queen)
        {
            Movements.Add(Movement.InfLeft());
            Movements.Add(Movement.InfRight());
            Movements.Add(Movement.InfUp());
            Movements.Add(Movement.InfDown());
            Movements.Add(Movement.InfUpLeft());
            Movements.Add(Movement.InfUpRight());
            Movements.Add(Movement.InfDownLeft());
            Movements.Add(Movement.InfDownRight());
        }
    }
}
